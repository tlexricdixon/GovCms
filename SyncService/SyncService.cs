using Blazored.LocalStorage;
using CmsModels;
using DbContexts;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace Service;
public class SyncService<T> : ISyncService<T> where T : SyncEntity
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _http;
    private readonly LocalDbContext _db;
    private readonly DbSet<T> _set;
    private static string QueueKey => $"offline-queue-{typeof(T).Name}";
    private const string HealthEndpoint = "api/health";

    public SyncService(ILocalStorageService localStorage, HttpClient http, LocalDbContext db)
    {
        _localStorage = localStorage;
        _http = http;
        _db = db;
        _set = _db.Set<T>();
    }

    public async Task<bool> IsOnlineAsync()
    {
        try
        {
            var result = await _http.GetAsync(HealthEndpoint);
            return result.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task EnqueueAsync(T item)
    {
        var queue = await _localStorage.GetItemAsync<List<T>>(QueueKey) ?? new List<T>();
        queue.Add(item);
        await _localStorage.SetItemAsync(QueueKey, queue);
    }

    public async Task SyncAsync(string endpoint)
    {
        var queue = await _localStorage.GetItemAsync<List<T>>(QueueKey);
        if (queue is null || queue.Count == 0)
            return;

        foreach (var item in queue.ToList())
        {
            try
            {
                var response = await _http.PostAsJsonAsync(endpoint, item);
                if (response.IsSuccessStatusCode)
                    queue.Remove(item);
            }
            catch
            {
                break; // Retry later on network error
            }
        }

        await _localStorage.SetItemAsync(QueueKey, queue);
    }

    public async Task PushChangesAsync(string endpoint)
    {
        var changes = await _set
            .Where(x => x.NeedsSync)
            .ToListAsync();

        if (changes.Any())
        {
            var response = await _http.PostAsJsonAsync(endpoint, changes);
            if (response.IsSuccessStatusCode)
            {
                foreach (var item in changes)
                    item.NeedsSync = false;

                await _db.SaveChangesAsync();
            }
        }
    }

    public async Task PullUpdatesAsync(string endpoint)
    {
        var updates = await _http.GetFromJsonAsync<List<T>>(endpoint);
        if (updates is null) return;

        foreach (var update in updates)
        {
            var local = await _set.FindAsync(update.Id);
            if (local is null)
            {
                _set.Add(update);
            }
            else if (update.LastModified > local.LastModified)
            {
                _db.Entry(local).CurrentValues.SetValues(update);
            }
        }

        await _db.SaveChangesAsync();
    }
}

