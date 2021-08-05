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
        IUserPermissionEvaluator On<T>(T entity);

        /// <summary>
        /// On the specified resource identifier.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        IUserPermissionEvaluator OnResource(string resourceName);

        IUserPermissionEvaluator OnResource(long resourceId);

        /// <summary>
        /// Called when [resource type].
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        IUserPermissionEvaluator OnResourceType(Type resourceType);
    }

}