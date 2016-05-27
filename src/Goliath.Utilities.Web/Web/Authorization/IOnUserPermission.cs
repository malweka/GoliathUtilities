using System;

namespace Goliath.Web.Authorization
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
        IUserPermissionEvaluator On<T>(T entity);

        /// <summary>
        /// Ons the specified resource identifier.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        IUserPermissionEvaluator OnResourceType(int resourceId, string resourceName);

        /// <summary>
        /// Called when [resource type].
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        IUserPermissionEvaluator OnResourceType(Type resourceType);
    }

}