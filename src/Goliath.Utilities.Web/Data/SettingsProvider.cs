using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Goliath.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Goliath.Data.ISettingsProvider" />
    public abstract class InMemoryCachedSettingsProvider : ISettingsProvider
    {

        /// <summary>
        /// The is loaded
        /// </summary>
        protected static bool isLoaded;

        static readonly ConcurrentDictionary<string, string> settingCache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Loads all.
        /// </summary>
        /// <param name="cache">The cache.</param>
        protected abstract void LoadAll(IDictionary<string, string> cache);

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected abstract void UpdateSetting(string key, string value);

        /// <summary>
        /// Adds the setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected abstract void AddSetting(string key, string value);

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Get(string key)
        {
            LoadAll(settingCache);

            string val;
            settingCache.TryGetValue(key, out val);
            return val;
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set(string key, string value)
        {
            LoadAll(settingCache);

            string val;
            if (settingCache.TryGetValue(key, out val))
            {
                UpdateSetting(key, val);
            }
            else
            {
                settingCache.TryAdd(key, value);
                AddSetting(key, value);
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            settingCache.Clear();
            isLoaded = false;

            LoadAll(settingCache);
        }

        /// <summary>
        /// Gets the configuration application setting.
        /// </summary>
        /// <param name="configKeyName">Name of the configuration key.</param>
        /// <returns></returns>
        public virtual string GetConfigFileSetting(string configKeyName)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(configKeyName);
        }
    }
}