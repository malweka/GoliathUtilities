using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Goliath.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    { 
        readonly MemoryCache memoryCache;

        public MemoryCacheProvider():this(MemoryCache.Default)
        {
        }

        public MemoryCacheProvider(MemoryCache? memoryCache)
        {
            memoryCache ??= MemoryCache.Default;

            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var value = memoryCache.Get(key);
            if(value != null)
                return (T)value;

            return default(T);
        }

        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Get<T>(key));
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        public bool Add<T>(string key, T value, TimeSpan? expiry = null)
        {
            memoryCache.Set(key, value,
                expiry.HasValue
                    ? new DateTimeOffset(DateTime.UtcNow.Add(expiry.Value))
                    : ObjectCache.InfiniteAbsoluteExpiration);

            return true;
        }

        public Task<bool> AddAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            Add(key, value, expiry);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Adds a range of keys and values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues">The key values.</param>
        /// <param name="expiry">The expiry.</param>
        public bool AddRange<T>(Dictionary<string, T> keyValues, TimeSpan? expiry = null)
        {
            bool success = true;
            foreach (var keyValue in keyValues)
            {
                if (!Add(keyValue.Key, keyValue.Value, expiry))
                    success = false;
            }
            return success;
        }

        public Task AddRangeAsync<T>(Dictionary<string, T> keyValues, TimeSpan? expiry = null)
        {
            AddRange(keyValues, expiry);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            var exists = memoryCache.Contains(key);
            if (exists)
                memoryCache.Remove(key);

            return exists;
        }

        public bool ContainsKey(string key)
        {
            return memoryCache.Contains(key);
        }

        public bool ExpireKey(string key, TimeSpan? lifetime)
        {
            var cacheItem = memoryCache.Get(key);
            if (cacheItem == null)
                return false;

            lifetime ??= new TimeSpan(0, 0, 0, 0, 1);

            return Add(key, cacheItem, lifetime);
        }
    }
}