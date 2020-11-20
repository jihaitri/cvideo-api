using CVideoAPI.Helpers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVideoAPI.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private static ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _cache;
        public CacheService(IDistributedCache distributedCache)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(AppConfig.GetRedisConnectionString());
            _cache = _connectionMultiplexer.GetDatabase();
            _distributedCache = distributedCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeTimeLive)
        {
            if (response == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeTimeLive
            });
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }

        public async Task RemoveCache(string cacheKey)
        {
            var server = _connectionMultiplexer.GetServer("cvideo-api.redis.cache.windows.net:6380");
            var keys = server.Keys(pattern: $"*{cacheKey.Split("/")[0]}*").ToArray();
            await _cache.KeyDeleteAsync(keys);
        }
    }
}
