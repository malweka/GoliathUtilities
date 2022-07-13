using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Goliath.Caching
{
    public static class StackExchangeRedisExtensions
    {
        private static readonly ILogger logger = Logger.GetLogger(typeof(StackExchangeRedisExtensions));

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T Get<T>(this IDatabase redisCache, string key)
        {
            var jsonSerialized = redisCache.StringGet(key);

            return string.IsNullOrWhiteSpace(jsonSerialized) ? default(T) : JsonConvert.DeserializeObject<T>(jsonSerialized);
        }

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this IDatabase redisCache, string key)
        {
            var jsonSerialized = await redisCache.StringGetAsync(key);
            return string.IsNullOrWhiteSpace(jsonSerialized) ? default(T) : JsonConvert.DeserializeObject<T>(jsonSerialized);
        }

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static object Get(this IDatabase redisCache, string key)
        {
            var jsonSerialized = redisCache.StringGet(key);
            return string.IsNullOrWhiteSpace(jsonSerialized) ? null : JsonConvert.DeserializeObject(jsonSerialized);
        }

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static async Task<object> GetAsync(this IDatabase redisCache, string key)
        {
            var jsonSerialized = await redisCache.StringGetAsync(key);
            return string.IsNullOrWhiteSpace(jsonSerialized) ? null : JsonConvert.DeserializeObject(jsonSerialized);
        }

        /// <summary>
        /// Sets the value of the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static bool Set<T>(this IDatabase redisCache, string key, T value, TimeSpan? expires = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializedObject = JsonConvert.SerializeObject(value, Formatting.None);
            return redisCache.StringSet(key, serializedObject, expires, When.Always, CommandFlags.FireAndForget);
        }

        /// <summary>
        /// Sets the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">value</exception>
        public static async Task SetAsync<T>(this IDatabase redisCache, string key, T value, TimeSpan? expires = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializedObject = JsonConvert.SerializeObject(value);
            await redisCache.StringSetAsync(key, serializedObject, expires, When.Always, CommandFlags.FireAndForget);
        }

        /// <summary>
        /// Sets the value of the specified key.
        /// </summary>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static void Set(this IDatabase redisCache, string key, object value, TimeSpan? expires = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializedObject = JsonConvert.SerializeObject(value);
            redisCache.StringSet(key, serializedObject, expires);
        }

        /// <summary>
        /// Sets the value of the specified key.
        /// </summary>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">value</exception>
        public static async Task SetAsync(this IDatabase redisCache, string key, object value, TimeSpan? expires = null)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializedObject = JsonConvert.SerializeObject(value);
            await redisCache.StringSetAsync(key, serializedObject, expires);
        }

        /// <summary>
        /// Sets the values of the specified keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="keyValues">The key values.</param>
        /// <param name="expires">The expires.</param>
        /// <exception cref="System.ArgumentNullException">T</exception>
        public static bool MSet<T>(this IDatabase redisCache, Dictionary<string, T> keyValues, TimeSpan? expires = null)
        {
            if (keyValues.Any(keyValue => keyValue.Value == null))
                throw new ArgumentNullException(nameof(T));

            var serializedObjects = keyValues.Select(keyValue =>
                new KeyValuePair<RedisKey, RedisValue>(keyValue.Key, JsonConvert.SerializeObject(keyValue.Value))).ToArray();
            return redisCache.StringSet(serializedObjects, When.Always, CommandFlags.FireAndForget);
        }

        /// <summary>
        /// Sets the values of the specified keys.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="keyValues">The key values.</param>
        /// <param name="expires">The expires.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">T</exception>
        public static async Task MSetAsync<T>(this IDatabase redisCache, Dictionary<string, T> keyValues, TimeSpan? expires = null)
        {
            if (keyValues.Any(keyValue => keyValue.Value == null))
                throw new ArgumentNullException(nameof(T));

            var serializedObjects = keyValues.Select(keyValue =>
                new KeyValuePair<RedisKey, RedisValue>(keyValue.Key, JsonConvert.SerializeObject(keyValue.Value))).ToArray();
            await redisCache.StringSetAsync(serializedObjects, When.Always, CommandFlags.FireAndForget);
        }

        /// <summary>
        /// Gets the values of the specified keys. If a key doesn't exist, it will be ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public static IList<T> MGet<T>(this IDatabase redisCache, RedisKey[] keys)
        {
            var jsonSerialized = redisCache.StringGet(keys).Select(key => key.ToString());

            IList<T> returnItems = new List<T>();
            foreach (var json in jsonSerialized)
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    continue;
                }

                try
                {
                    returnItems.Add(JsonConvert.DeserializeObject<T>(json));
                }
                catch (JsonReaderException ex)
                {
                    logger.Log(LogLevel.Warning, $"Failed to deserialize the cache value: {json}", ex);
                }
            }

            return returnItems;
        }

        /// <summary>
        /// Gets the values of the specified keys. If a key doesn't exist, it will be ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisCache">The redis cache.</param>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public static async Task<IList<T>> MGetAsync<T>(this IDatabase redisCache, RedisKey[] keys)
        {
            var redisValues = await redisCache.StringGetAsync(keys);
            var jsonSerialized = redisValues.Select(key => key.ToString());

            IList<T> returnItems = new List<T>();
            foreach (var json in jsonSerialized)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        continue;
                    }

                    returnItems.Add(JsonConvert.DeserializeObject<T>(json));
                }
                catch (JsonReaderException ex)
                {
                    logger.Log(LogLevel.Warning, $"Failed to deserialize the cache value: {json}", ex);
                }
            }

            return returnItems;
        }
    }
}