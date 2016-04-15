using System;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOnUserPermission
    {
        /// <summary>
        /// Ons the specified entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        IUserPermissionProvider On<T>(T entity);

        /// <summary>
        /// Ons the specified resource identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        IUserPermissionProvider OnResourceType(int resourceTypeId, string resourceName);

        /// <summary>
        /// Called when [resource type].
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        IUserPermissionProvider OnResourceType(Type resourceType);
    }

    ///// <summary>
    ///// 
    ///// </summary>
    //public interface IOnRolePermission
    //{
    //    /// <summary>
    //    /// Ons the specified entity.
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="entity">The entity.</param>
    //    /// <returns></returns>
    //    IRolePermissionProvider On<T>(T entity);

    //    /// <summary>
    //    /// Ons the specified resource identifier.
    //    /// </summary>
    //    /// <param name="resourceTypeId">The resource type identifier.</param>
    //    /// <param name="resourceName">Name of the resource.</param>
    //    /// <returns></returns>
    //    IRolePermissionProvider OnResourceType(int resourceTypeId, string resourceName);

    //    /// <summary>
    //    /// Called when [resource type].
    //    /// </summary>
    //    /// <param name="resourceType">Type of the resource.</param>
    //    /// <returns></returns>
    //    IRolePermissionProvider OnResourceType(Type resourceType);
    //}
}