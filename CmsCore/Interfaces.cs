// =====================
// [Scaffolded Interfaces]
// =====================

using CmsModels;

namespace Interfaces;
public interface ILocalDatabase
{
    Task InitializeAsync();
    Task<List<T>> GetAllAsync<T>() where T : class;
    Task SaveAsync<T>(T item) where T : class;
}

/// <summary>
/// Generic sync service for pushing/pulling changes of entities implementing ISyncable.
/// </summary>
/// <typeparam name="T">Entity type implementing ISyncable</typeparam>
public interface ISyncService<T> where T : SyncEntity
{
    /// <summary>
    /// Checks online status before attempting sync.
    /// </summary>
    Task<bool> IsOnlineAsync();

    /// <summary>
    /// Pushes local changes of type T to the remote source.
    /// </summary>
    Task PushChangesAsync(string endpoint);

    /// <summary>
    /// Pulls latest remote updates of type T and applies them locally.
    /// </summary>
    Task PullUpdatesAsync(string endpoint);
}

