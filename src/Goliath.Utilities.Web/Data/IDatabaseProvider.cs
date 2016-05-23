using Goliath.Data;

namespace Goliath.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Gets the session factory.
        /// </summary>
        /// <value>
        /// The session factory.
        /// </value>
        ISessionFactory SessionFactory { get; }

        /// <summary>
        /// Initializes the specified map file name.
        /// </summary>
        /// <param name="mapFileName">Name of the map file.</param>
        /// <param name="entityNamespace">The entity namespace.</param>
        void Init(string mapFileName = "data.map.config", string entityNamespace = "Goliath.Data");
    }
}