using System;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITypeToResourceMapper
    {
        /// <summary>
        /// Resolves the type of the resource type identifier from.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        int ResolveResourceIdFromType(Type type);

        /// <summary>
        /// Resolves the type from resource type identifier.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <returns></returns>
        Type ResolveTypeFromResourceId(int resourceId);
    }
}