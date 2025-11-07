namespace PortfolioClient.Wasm.Services;

public interface ICacheService
{
    /// <summary>
    /// Gets a cached value by key
    /// </summary>
    /// <typeparam name="T">Type of the cached value</typeparam>
    /// <param name="key">Cache key</param>
    /// <returns>Cached value or default if not found or expired</returns>
    T? Get<T>(string key);

    /// <summary>
    /// Sets a value in the cache with expiration
    /// </summary>
    /// <typeparam name="T">Type of the value to cache</typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">Value to cache</param>
    /// <param name="expirationMinutes">Minutes until expiration (default 5)</param>
    void Set<T>(string key, T value, int expirationMinutes = 5);

    /// <summary>
    /// Removes a value from the cache
    /// </summary>
    /// <param name="key">Cache key</param>
    void Remove(string key);

    /// <summary>
    /// Clears all cached values
    /// </summary>
    void Clear();
}
