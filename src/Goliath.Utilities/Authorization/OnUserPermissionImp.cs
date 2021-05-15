using System;

namespace Goliath.Authorization
{
    class OnUserPermissionImp : IOnUserPermission
    {
        private readonly IAppUser user;
        private readonly IResourcePermissionGroupMapper secProv;
        private readonly IPermissionStore permissionStore;

        public OnUserPermissionImp(IAppUser user, IPermissionStore permissionStore, IResourcePermissionGroupMapper secProv)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (permissionStore == null) throw new ArgumentNullException(nameof(permissionStore));
            if (secProv == null) throw new ArgumentNullException(nameof(secProv));

            this.user = user;
            this.permissionStore = permissionStore;
            this.secProv = secProv;
        }

        public IUserPermissionEvaluator On<T>(T entity)
        {
            var type = typeof (T);
            var resourceId = secProv.GetResourceGroupIdByType(type);
            if (resourceId < 1)
                throw new SecurityException($"Resource type Id not found for type {type}");

            return new PermissionEvaluator(user, resourceId, permissionStore);
        }

        public IUserPermissionEvaluator OnResourceType(int resourceId, string resourceName)
        {
            return new PermissionEvaluator(user, resourceId, permissionStore);
        }

        public IUserPermissionEvaluator OnResourceType(Type resourceType)
        {
            var resourceId = secProv.GetResourceGroupIdByType(resourceType);
            if (resourceId < 1)
                throw new SecurityException($"Resource type Id not found for type {resourceType}");

            return new PermissionEvaluator(user, resourceId, permissionStore);
        }
    }
}