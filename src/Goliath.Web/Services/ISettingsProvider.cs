namespace Goliath.Web.Services
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
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Set(string key, string value);

        /// <summary>
        /// Resets this instance.
        /// </summary>
        void Reset();
    }
}