using System;
using System.Collections.Generic;
using Goliath.Models;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IPermissionStore" />
    public abstract class BasePermissionStore : IPermissionStore
    {
        /// <summary>
        /// True if the permissions are loaded
        /// </summary>
        protected static bool IsLoaded;

        /// <summary>
        /// The permission database
        /// </summary>
        protected IPermissionDataAdapter PermissionDb;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePermissionStore"/> class.
        /// </summary>
        /// <param name="permissionDataAdapter">The permission data adapter.</param>
        protected BasePermissionStore(IPermissionDataAdapter permissionDataAdapter)
        {
            PermissionDb = permissionDataAdapter ?? throw new ArgumentNullException(nameof(permissionDataAdapter));
        }

        /// <summary>
        /// Verifies that the permission are loaded.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Permission cache must be loaded before performing this operation. Please call the LoadPermission method first.</exception>
        protected void VerifyPermissionAreLoaded()
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Permission cache must be loaded before performing this operation. Please call the LoadPermission method first.");
        }

        /// <summary>
        /// Loads the permissions.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Caches the permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        protected abstract void CachePermission(IPermissionItem permission);

        /// <summary>
        /// Reloads the permissions.
        /// </summary>
        public abstract void Reload();

        /// <summary>
        /// Adds the permission.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        public virtual void AddPermission(long resourceId, long roleNumber, int action)
        {
            VerifyPermissionAreLoaded();

            var permission = GetPermission(resourceId, roleNumber);

            if (permission == null)
            {
                //permission was not found let's create it.
                permission = PermissionDb.Insert(resourceId, roleNumber, action);
                //cache new permission
                CachePermission(permission);
            }
            else
            {
                if ((permission.PermValue & action) == action) return;
                //add the permission 
                permission.PermValue |= action;
                //update the database with the changes
                PermissionDb.Update(permission);
            }
        }

        /// <summary>
        /// Modifies the permission.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="action">The action.</param>
        public virtual void RemovePermission(long resourceId, long roleNumber, int action)
        {
            VerifyPermissionAreLoaded();

            var permission = GetPermission(resourceId, roleNumber);
            if (permission == null) return;

            if ((permission.PermValue & action) != action) return;

            //remove permission
            permission.PermValue ^= action;
            //update the database with the changes
            PermissionDb.Update(permission);
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="roleNumber">The role Number.</param>
        /// <returns></returns>
        public abstract IPermissionItem GetPermission(long resourceId, long roleNumber);

        /// <summary>
        /// Updates the role permissions.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="permissionModels">The permission models.</param>
        /// <param name="context">The context.</param>
        public abstract void UpdateRolePermissions(IRole role, IList<PermissionActionModel> permissionModels, UserContext context = null);

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <returns></returns>
        public abstract PermissionList GetPermissions(long resourceId);
    }
}