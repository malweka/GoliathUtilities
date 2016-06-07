using System;
using System.Collections.Concurrent;

namespace Goliath.Data
{
    public class InMemoryCache : ICacheProvider
    {
        static readonly ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">key</exception>
        public T Get<T>(string key)
        {
            if (key == null) throw new ArgumentNullException("key");
            object obj;
            cache.TryGetValue(key, out obj);
            return (T)obj;
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="System.ArgumentNullException">key</exception>
        public void Set<T>(string key, T value)
        {
            if (key == null) throw new ArgumentNullException("key");

            object obj;
            if (cache.TryGetValue(key, out obj))
            {
                cache[key] = value;
            }
            else
            {
                cache.TryAdd(key, value);
            }
        }

        public bool Remove(string key)
        {
            object val;
            return cache.TryRemove(key, out val);
        }

        public void ReleaseAll()
        {
            cache.Clear();
        }
    }
}