using Goliath.Models;

namespace Goliath
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
        string GetConfigFileSetting(string configKeyName);

        ConnectionStringInfo GetConnectionString(string connectionStringName);

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="editorType">Type of the editor.</param>
        void Set(string key, string value, VisualEditorType editorType);

        void Reset();
    }

    public struct ConnectionStringInfo
    {
        public string ProviderName;
        public string ConnectionString;
    }
}