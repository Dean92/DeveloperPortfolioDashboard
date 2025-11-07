namespace PortfolioClient.Wasm.Services;

public class CacheService : ICacheService
{
    private readonly Dictionary<string, CacheEntry> _cache = new();
    private readonly ILogger<CacheService> _logger;

    public CacheService(ILogger<CacheService> logger)
    {
        _logger = logger;
    }

    public T? Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out var entry))
        {
            if (entry.ExpiresAt > DateTime.UtcNow)
            {
                _logger.LogDebug("Cache hit for key: {Key}", key);
                return (T?)entry.Value;
            }
            else
            {
                _logger.LogDebug("Cache entry expired for key: {Key}", key);
                _cache.Remove(key);
            }
        }

        _logger.LogDebug("Cache miss for key: {Key}", key);
        return default;
    }

    public void Set<T>(string key, T value, int expirationMinutes = 5)
    {
        var entry = new CacheEntry
        {
            Value = value,
            ExpiresAt = DateTime.UtcNow.AddMinutes(expirationMinutes),
        };

        _cache[key] = entry;
        _logger.LogDebug(
            "Cached value for key: {Key}, expires at: {ExpiresAt}",
            key,
            entry.ExpiresAt
        );
    }

    public void Remove(string key)
    {
        if (_cache.Remove(key))
        {
            _logger.LogDebug("Removed cache entry for key: {Key}", key);
        }
    }

    public void Clear()
    {
        var count = _cache.Count;
        _cache.Clear();
        _logger.LogInformation("Cleared {Count} cache entries", count);
    }

    private class CacheEntry
    {
        public object? Value { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
