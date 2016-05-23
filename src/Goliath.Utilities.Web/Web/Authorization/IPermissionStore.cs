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
        void LoadPermissions();

        void ReloadPermissions();

        /// <summary>
        /// Adds the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="description">The description.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        void AddPermission(int resourceTypeId, string resourceName, string description, int roleNumber, int action);

        /// <summary>
        /// Modifies the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        void ModifyPermission(int resourceTypeId, int roleNumber, int action);

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        IUserRolePerm GetPermission(int resourceTypeId, int roleId);

        /// <summary>
        /// Determines whether this instance [can perform action] the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="user">The user.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        bool CanPerformAction(int resourceTypeId, IAppUser user, int action);

        /// <summary>
        /// Removes the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="role">The role.</param>
        void RemovePermission(int resourceTypeId, int role);

        /// <summary>
        /// Updates the role permission.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="permisionModels">The permision models.</param>
        /// <param name="context">The context.</param>
        void UpdateRolePermission(IRole role, IList<PermissionActionModel> permisionModels, ApplicationContext context);
    }
}