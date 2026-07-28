using Blazored.LocalStorage;
using CmsModels;
using DbContexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Contrib.HttpClient;
using Service;
using System.Net;

namespace CmsTests;
public class SyncServiceTests
{
    private readonly Mock<ILocalStorageService> _localStorageMock = new();
    private readonly Mock<HttpMessageHandler> _httpHandlerMock = new();
    private readonly HttpClient _httpClient;
    private readonly LocalDbContext _dbContext;

    public SyncServiceTests()
    {
        _httpClient = new HttpClient(_httpHandlerMock.Object)
        {
            BaseAddress = new Uri("http://localhost")
        };

        var options = new DbContextOptionsBuilder<LocalDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        _dbContext = new LocalDbContext(options);
    }

    [Fact]
    public async Task IsOnlineAsync_ReturnsTrue_WhenHealthEndpointIsOk()
    {
        _httpHandlerMock
            .SetupRequest(HttpMethod.Get, "http://localhost/api/health")
            .ReturnsResponse(HttpStatusCode.OK);

        var service = new SyncService<Post>(_localStorageMock.Object, _httpClient, _dbContext);
        var result = await service.IsOnlineAsync();

        Assert.True(result);
    }

    [Fact]
    public async Task EnqueueAsync_AddsItemToQueue()
    {
        var post = new Post 
        { 
            Id = 1, 
            Title = "Test",
            Slug = "test-slug",
            Content = "Test content",
            Excerpt = "Test excerpt",
            Author = "Test author"
        };
        var service = new SyncService<Post>(_localStorageMock.Object, _httpClient, _dbContext);
        await service.EnqueueAsync(post);

        _localStorageMock.Verify(x => x.SetItemAsync<List<Post>>(
            It.IsAny<string>(),
            It.Is<List<Post>>(l => l.Contains(post)),
            It.IsAny<CancellationToken>()), // Explicitly specify the optional argument  
            Times.Once);
    }

    // Add more tests for SyncAsync, PushChangesAsync, PullUpdatesAsync as needed
}

