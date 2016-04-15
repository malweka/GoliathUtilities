using Goliath.Models;

namespace Goliath.Authorization
{
    class PermissionProvider : IUserPermissionProvider
    {
        private readonly IAppUser user;
        private readonly int resourceTypeId;
        private readonly IPermissionStore permissionStore;

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>
        /// The name of the resource.
        /// </value>
        public string ResourceName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionProvider"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="permissionStore">The permission store.</param>
        public PermissionProvider(IAppUser user, int resourceTypeId, IPermissionStore permissionStore)
        {
            this.user = user;
            this.resourceTypeId = resourceTypeId;
            this.permissionStore = permissionStore;
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public bool CanPerformAction(PermActionType action)
        {
            return permissionStore.CanPerformAction(resourceTypeId, user, action);
        }

        //public void RemovePermission()
        //{
        //    permissionStore.RemovePermission(resourceTypeId, user);
        //}
    }
}