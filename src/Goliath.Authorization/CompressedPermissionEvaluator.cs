using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IUserPermissionEvaluator" />
    public class CompressedPermissionEvaluator : IUserPermissionEvaluator
    {
        private readonly long resourceId;
        private readonly IPermissionStore permissionStore;
        private readonly ILogger<CompressedPermissionEvaluator> logger;

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public IAppUser User { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressedPermissionEvaluator" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="permissionStore">The permission store.</param>
        /// <param name="logger"></param>
        public CompressedPermissionEvaluator(IAppUser user, long resourceId, IPermissionStore permissionStore, ILogger<CompressedPermissionEvaluator> logger)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            this.resourceId = resourceId;
            this.permissionStore = permissionStore ?? throw new ArgumentNullException(nameof(permissionStore));
            this.logger = logger;
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public bool EvaluatePermission(long action)
        {
            if (User.Roles == null || User.Roles.Count == 0)
            {
                logger.LogInformation("User {user} is not associated to any role. Permission is therefore denied.", User.Username);
                return false;
            }

            //if it's an admin user we don't need to evaluate the permissions
            if (User.Roles.Any(c => c.Value.IsAdminRole))
            {
                logger.LogInformation("User {user} is associated to an admin Role. Permissions will not be evaluated and access granted by default.", User.Username);
                return true;
            }

            var permissionList = permissionStore.GetPermissions(resourceId);

            foreach (var userRole in User.Roles)
            {
                if (!permissionList.TryGetValue(userRole.Value.RoleNumber, out IPermissionItem permission))
                    continue;

                var perm = (permission.PermValue & action) == action;
                if (perm) return true;
            }

            return false;
        }
    }
}