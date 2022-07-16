using Microsoft.Extensions.Caching.Memory;
using MoserBlog.MediaTool.Api.Handler.Interfaces;

namespace MoserBlog.MediaTool.Api.Handler;

public class CacheHandler : ICacheHandler
{
    private readonly IMemoryCache _memoryCache;

    public CacheHandler(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }


    public T TryGetCacheEntry<T>(Func<T> cachableFunc, params object[] parameters)
    {
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
                AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(5)
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
