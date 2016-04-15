namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Gets the configuration application setting.
        /// </summary>
        /// <param name="configKeyName">Name of the configuration key.</param>
        /// <returns></returns>
        string GetConfigAppSetting(string configKeyName);

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set(string key, string value);

        void Reset();
    }
}