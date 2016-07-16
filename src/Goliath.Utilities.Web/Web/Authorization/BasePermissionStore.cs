using System;
using System.Collections.Generic;
using Goliath.Models;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Goliath.Web.Authorization.IPermissionStore" />
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
            if (permissionDataAdapter == null) throw new ArgumentNullException(nameof(permissionDataAdapter));
            PermissionDb = permissionDataAdapter;
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
        public virtual void AddPermission(int resourceId, int roleNumber, int action)
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
                permission.PermValue = permission.PermValue | action;
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
        public virtual void RemovePermission(int resourceId, int roleNumber, int action)
        {
            VerifyPermissionAreLoaded();

            var permission = GetPermission(resourceId, roleNumber);
            if (permission == null) return;

            if ((permission.PermValue & action) != action) return;

            //remove permission
            permission.PermValue = permission.PermValue ^ action;
            //update the database with the changes
            PermissionDb.Update(permission);
        }

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="rolenumber">The rolenumber.</param>
        /// <returns></returns>
        public abstract IPermissionItem GetPermission(int resourceId, int rolenumber);

        /// <summary>
        /// Updates the role permissions.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="permisionModels">The permision models.</param>
        /// <param name="context">The context.</param>
        public abstract void UpdateRolePermissions(IRole role, IList<PermissionActionModel> permisionModels, ApplicationContext context = null);

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <returns></returns>
        public abstract PermissionList GetPermissions(int resourceId);
    }
}