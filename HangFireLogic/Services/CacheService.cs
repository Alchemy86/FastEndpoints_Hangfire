using Microsoft.Extensions.Caching.Memory;

namespace HangFireLogic.Services;

public class CacheService
{
    public IMemoryCache Cache { get; }

    public CacheService(IMemoryCache cache)
    {
        Cache = cache;
        Cache.Set("Paused", true);
    }
}