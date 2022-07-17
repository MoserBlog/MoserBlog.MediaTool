namespace MoserBlog.MediaTool.Api.Handler.Interfaces;

public interface ICacheHandler
{
    T TryGetCacheEntry<T>(Func<T> cachableFunc, int cacheDurationInMinutes = 1, params object[] parameters);
}
