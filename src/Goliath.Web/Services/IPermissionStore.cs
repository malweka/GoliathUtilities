using System.Collections.Generic;

namespace Goliath.Web.Services
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

        /// <summary>
        /// Adds the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="description">The description.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        void AddPermission(int resourceTypeId, string resourceName, string description, int roleNumber, PermActionType action);

        /// <summary>
        /// Modifies the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        void ModifyPermission(int resourceTypeId, int roleNumber, PermActionType action);

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        /// <returns></returns>
        IUserRolePermission GetPermission(int resourceTypeId, int roleId);

        /// <summary>
        /// Determines whether this instance [can perform action] the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="user">The user.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        bool CanPerformAction(int resourceTypeId, IAppUser user, PermActionType action);

        /// <summary>
        /// Removes the permission.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="role">The role.</param>
        void RemovePermission(int resourceTypeId, int role);

        void UpdateRolePermission(IRole role, IList<PermissionActionModel> permisionModels, AppContext context);
    }
}