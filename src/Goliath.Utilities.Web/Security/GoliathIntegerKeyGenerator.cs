using System;
using Goliath.Data.Mapping;
using Goliath.Data.Providers;
using Goliath.Data.Sql;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class GoliathIntegerKeyGenerator : IKeyGenerator<long>
    {
        /// <summary>
        /// The generator name
        /// </summary>
        public const string GeneratorName = "Cms_Integer_keyGen";

        #region IKeyGenerator<long> Members

        public long Generate(SqlDialect sqlDialect, EntityMap entityMap, string propertyName, out  SqlOperationPriority priority)
        {
            priority = SqlOperationPriority.Low;
            var uniqueIdGenerator = UniqueIdFactory.CreateIdGenerator();
            return uniqueIdGenerator.GetNextId();
        }

        #endregion

        #region IKeyGenerator Members

        /// <summary>
        /// Gets the type of the key.
        /// </summary>
        /// <value>
        /// The type of the key.
        /// </value>
        public Type KeyType
        {
            get { return typeof(long); }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return GeneratorName; }
        }

        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <param name="sqlDialect">The SQL dialect.</param>
        /// <param name="entityMap">The entity map.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="priority">The priority.</param>
        /// <returns></returns>
        object IKeyGenerator.GenerateKey(SqlDialect sqlDialect, EntityMap entityMap, string propertyName, out SqlOperationPriority priority)
        {
            return Generate(sqlDialect, entityMap, propertyName, out priority);
        }

        /// <summary>
        /// Gets a value indicating whether this instance is database generated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is database generated; otherwise, <c>false</c>.
        /// </value>
        public bool IsDatabaseGenerated
        {
            get { return false; }
        }

        #endregion
    }
}