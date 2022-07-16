namespace MoserBlog.MediaTool.Api.Handler.Interfaces;

public interface ICacheHandler
{
    T TryGetCacheEntry<T>(Func<T> cachableFunc, params object[] parameters);
}
