using System;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResourcePermissionGroupMapper
    {

        /// <summary>
        /// Gets the type of the resource group identifier by.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        int GetResourceGroupIdByType(Type type);

        /// <summary>
        /// Gets the name of the resource group identifier by.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        int? GetResourceGroupIdByName(string resourceName);
    }
}