using System;

namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResourceTypeMap
    {
        /// <summary>
        /// Resolves the type of the resource type identifier from.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        int ResolveResourceTypeIdFromType(Type type);

        /// <summary>
        /// Resolves the type from resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <returns></returns>
        Type ResolveTypeFromResourceTypeId(int resourceTypeId);
    }
}