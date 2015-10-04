namespace Goliath.Web.Services
{
    class PermissionProvider : IUserPermissionProvider
    {
        private readonly IAppUser user;
        private readonly int resourceTypeId;
        private readonly IPermissionStore permissionStore;

        public string ResourceName { get; set; }

        public PermissionProvider(IAppUser user, int resourceTypeId, IPermissionStore permissionStore)
        {
            this.user = user;
            this.resourceTypeId = resourceTypeId;
            this.permissionStore = permissionStore;
        }

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