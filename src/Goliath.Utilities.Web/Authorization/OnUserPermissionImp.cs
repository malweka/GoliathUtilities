using System;
using System.Security;

namespace Goliath.Authorization
{
    class OnUserPermissionImp : IOnUserPermission
    {
        private readonly IAppUser user;
        private readonly IResourceSecurtityProvider secProv;
        private readonly IPermissionStore permissionStore;

        public OnUserPermissionImp(IAppUser user, IPermissionStore permissionStore, IResourceSecurtityProvider secProv)
        {
            if (user == null) throw new ArgumentNullException("user");
            //if (dbProvider == null) throw new ArgumentNullException("dbProvider");

            this.user = user;
            this.permissionStore = permissionStore;
            this.secProv = secProv;
            //this.dbProvider = dbProvider;
        }

        public IUserPermissionProvider On<T>(T entity)
        {
            var type = typeof (T);
            var resourceTypeId = secProv.ResolveResourceTypeIdFromType(type);
            if (resourceTypeId < 1)
                throw new SecurityException(string.Format("Resource type Id not found for type {0}", type));

            return new PermissionProvider(user, resourceTypeId, permissionStore) {ResourceName = type.FullName};
        }

        public IUserPermissionProvider OnResourceType(int resourceTypeId, string resourceName)
        {
            return new PermissionProvider(user, resourceTypeId, permissionStore) {ResourceName = resourceName};
        }

        public IUserPermissionProvider OnResourceType(Type resourceType)
        {
            var resourceTypeId = secProv.ResolveResourceTypeIdFromType(resourceType);
            if (resourceTypeId < 1)
                throw new SecurityException(string.Format("Resource type Id not found for type {0}", resourceType));

            return new PermissionProvider(user, resourceTypeId, permissionStore) {ResourceName = resourceType.FullName};
        }
    }

    public interface IResourceSecurtityProvider
    {
        int ResolveResourceTypeIdFromType(Type type);
        Type ResolveTypeFromResourceTypeId(int resourceTypeId);
    }
}