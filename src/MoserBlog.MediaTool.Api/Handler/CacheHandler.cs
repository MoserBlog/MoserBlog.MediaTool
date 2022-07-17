using Microsoft.Extensions.Caching.Memory;
using MoserBlog.MediaTool.Api.Handler.Interfaces;

namespace MoserBlog.MediaTool.Api.Handler;

public class CacheHandler : ICacheHandler
{
    private readonly IMemoryCache _memoryCache;
    private readonly IConfiguration _configuration;

    public CacheHandler(IMemoryCache memoryCache,
        IConfiguration configuration)
    {
        _memoryCache = memoryCache;
        _configuration = configuration;
    }


    public T TryGetCacheEntry<T>(Func<T> cachableFunc, int cacheDurationInMinutes = 1, params object[] parameters)
    {
        if (!bool.TryParse(_configuration["CachingActive"], out var cachingActive) || !cachingActive)
        {
            return cachableFunc();
        }

        var cacheKey = GetCacheKey(cachableFunc, parameters);

        if (_memoryCache.TryGetValue(cacheKey, out T cachedResult))
        {
            return cachedResult;
        }
        
        try
        {
            var result = cachableFunc();

            MemoryCacheEntryOptions cacheExpiration = new()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(cacheDurationInMinutes * 3),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(cacheDurationInMinutes)
            };

            _memoryCache.Set(cacheKey, result, cacheExpiration);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    private string GetCacheKey<T>(Func<T> function, params object[] parameters)
    {
        return $"{function.Method.Name}__{typeof(T).FullName}__{string.Join("_", parameters)}";
    }
}
