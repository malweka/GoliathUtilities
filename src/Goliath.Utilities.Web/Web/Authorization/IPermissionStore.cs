using System.Collections.Generic;
using Goliath.Models;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionStore
    {
        /// <summary>
        /// Loads the permissions.
        /// </summary>
        void Load();

        /// <summary>
        /// Reloads the permissions.
        /// </summary>
        void Reload();

        /// <summary>
        /// Adds the permission.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        void AddPermission(int resourceId, int roleNumber, int action);

        /// <summary>
        /// Modifies the permission.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        void RemovePermission(int resourceId, int roleNumber, int action);

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        IPermissionItem GetPermission(int resourceId, int roleId);

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <returns></returns>
        PermissionList GetPermissions(int resourceId);

        /// <summary>
        /// Updates the role permissions.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="permisionModels">The permision models.</param>
        /// <param name="context">The context.</param>
        void UpdateRolePermissions(IRole role, IList<PermissionActionModel> permisionModels, ApplicationContext context = null);

    }
}