using System;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Cache
{
    public interface ICacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive);
        Task<string> GetCachedResponseAsync(string cacheKey);
        Task RemoveCache(string cacheKey);
    }
}
