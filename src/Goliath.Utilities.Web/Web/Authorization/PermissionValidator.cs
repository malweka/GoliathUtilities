using System;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// This class is a facade for permissions
    /// </summary>
    public class PermissionValidator
    {
        private readonly IPermissionBuilder permissionBuilder;
        private readonly IAppUser user;

        /// <summary>
        /// Gets the permission service.
        /// </summary>
        /// <value>
        /// The permission service.
        /// </value>
        public IPermissionBuilder PermissionBuilder
        {
            get { return permissionBuilder; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionValidator"/> class.
        /// </summary>
        /// <param name="permissionBuilder">The permission service.</param>
        /// <param name="user">The user.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentNullException">
        /// permissionService
        /// or
        /// user
        /// </exception>
        public PermissionValidator(IPermissionBuilder permissionBuilder, IAppUser user)
        {
            if (permissionBuilder == null) throw new ArgumentNullException(nameof(permissionBuilder));
            if (user == null) throw new ArgumentNullException(nameof(user));

            this.permissionBuilder = permissionBuilder;
            this.user = user;
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified resource type identifier.
        /// </summary>
        /// <param name="resourceId">The resource type identifier.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public bool CanPerformAction(int resourceId, int action)
        {
            return permissionBuilder.For(user)
                .OnResourceType(resourceId, string.Empty)
                .EvaluatePermission(action);
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified resource type.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public bool CanPerformAction(Type resourceType, int action)
        {
            return permissionBuilder.For(user)
                .OnResourceType(resourceType)
                .EvaluatePermission(action);
        }

        
    }
}