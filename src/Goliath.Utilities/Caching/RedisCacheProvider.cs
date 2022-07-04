using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Goliath.Caching
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly IDatabase db;

        public RedisCacheProvider(IConnectionMultiplexer connectionMultiplexer)
        {
            if (connectionMultiplexer == null) throw new ArgumentNullException(nameof(connectionMultiplexer));
            db = connectionMultiplexer.GetDatabase(0);
        }

        public RedisCacheProvider(IDatabase db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }


        public IDatabase Database => db;

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {

            if (!db.KeyExists(key))
                return default(T);

            return db.Get<T>(key);
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            if (!db.KeyExists(key))
                return default(T);

            return await db.GetAsync<T>(key);
        }

        /// <summary>
        /// Adds the specified key.
        /// By Default, returns false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        public bool Add<T>(string key, T value, TimeSpan? expiry = null)
        {
            return db.Set(key, value, expiry);
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public async Task<bool> AddAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            await db.SetAsync(key, value, expiry);
            return true;
        }

        /// <summary>
        /// Add specified member to the set stored at key.
        /// By Default, returns false
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetAdd(string key, string value)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            if (value is null)
                throw new ArgumentNullException(nameof(value));

            return db.SetAdd(key, value, CommandFlags.FireAndForget);
        }

        /// <summary>
        /// Add specified member to the set stored at key async.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task SetAddAsync(string key, string value)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            if (value is null)
                throw new ArgumentNullException(nameof(value));

            await db.SetAddAsync(key, value, CommandFlags.FireAndForget);
        }

        /// <summary>
        /// Adds a range of keys and values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues">The key values.</param>
        /// <param name="expiry">The expiry.</param>
        public bool AddRange<T>(Dictionary<string, T> keyValues, TimeSpan? expiry = null)
        {
            return db.MSet(keyValues, expiry);
        }

        /// <summary>
        /// Adds the range asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues">The key values.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public async Task AddRangeAsync<T>(Dictionary<string, T> keyValues, TimeSpan? expiry = null)
        {
            await db.MSetAsync(keyValues, expiry);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return db.KeyDelete(key, CommandFlags.FireAndForget);
        }


        public bool ContainsKey(string key)
        {
            return db.KeyExists(key);
        }

        public bool ExpireKey(string key, TimeSpan? lifetime)
        {
            lifetime ??= new TimeSpan(0, 0, 0, 0, 1);
            return db.KeyExpire(key, lifetime);
        }

   
    }
}