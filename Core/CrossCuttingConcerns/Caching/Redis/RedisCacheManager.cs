using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;

        public RedisCacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return JsonConvert.DeserializeObject<T>(value);
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"Deserialization error: {ex.Message}");
                    return default; // T'nin varsayılan değeri (örneğin, referans türleri için null)
                }
            }
            return default;
        }

        public void Add<T>(string key, T data, int duration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(duration)
            };
            var jsonData = JsonConvert.SerializeObject(data);
            _cache.SetString(key, jsonData, options);
        }

        public bool IsAdd(string key)
        {
            var value = _cache.GetString(key);
            return !string.IsNullOrEmpty(value);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            // Microsoft.Extensions.Caching.StackExchangeRedis RedisCache unfortunately does not provide a direct way to remove by pattern.
            // You would need to implement your own mechanism to keep track of keys, or use server-side Redis commands via the ConnectionMultiplexer if necessary.
            throw new NotImplementedException("Remove by pattern is not directly supported by IDistributedCache.");
        }
    }
}
