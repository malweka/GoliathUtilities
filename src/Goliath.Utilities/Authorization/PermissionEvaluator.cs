using System;
using System.Collections.Generic;
using System.Linq;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IUserPermissionEvaluator" />
    public class PermissionEvaluator : IUserPermissionEvaluator
    {
        private readonly int resourceId;
        private readonly IPermissionStore permissionStore;
        private string adminRoleName;

        private readonly Dictionary<int, IRole> userRoles;

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public IAppUser User { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionEvaluator" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="permissionStore">The permission store.</param>
        /// <param name="adminRoleName">Name of the admin role.</param>
        public PermissionEvaluator(IAppUser user, int resourceId, IPermissionStore permissionStore, string adminRoleName = "Admin")
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (permissionStore == null) throw new ArgumentNullException(nameof(permissionStore));

            User = user;
            this.resourceId = resourceId;
            this.permissionStore = permissionStore;
            this.adminRoleName = adminRoleName;

            if (user.Roles != null)
                userRoles = user.Roles.Values.ToDictionary(c => c.RoleNumber);
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public bool EvaluatePermission(int action)
        {
            if (User?.Roles == null || User.Roles.Count == 0)
                return false;

            //if it's an admin user we don't need to evaluate the permissions
            if (User.Roles.ContainsKey(adminRoleName)) 
                return true;

            var permissionList = permissionStore.GetPermissions(resourceId);

            for (var i = 0; i < permissionList.Count; i++)
            {
                var permission = permissionList[i];
                if (!userRoles.ContainsKey(permission.RoleNumber)) continue;

                var perm = (permission.PermValue & action) == action;
                if (perm) return true;
            }

            return false;
        }

    }
}