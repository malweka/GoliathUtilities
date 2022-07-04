using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Goliath.Caching
{
    public interface ICacheProvider
    {
        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        bool Add<T>(string key, T value, TimeSpan? expiry = null);

        Task<bool> AddAsync<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// Adds a range of keys and values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues">The key values.</param>
        /// <param name="expiry">The expiry.</param>
        bool AddRange<T>(Dictionary<string, T> keyValues, TimeSpan? expiry = null);

        /// <summary>
        /// Adds the range asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues">The key values.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        Task AddRangeAsync<T>(Dictionary<string, T> keyValues, TimeSpan? expiry = null);

        bool ContainsKey(string key);

        bool Remove(string key);

        bool ExpireKey(string key, TimeSpan? lifetime);
    }
}