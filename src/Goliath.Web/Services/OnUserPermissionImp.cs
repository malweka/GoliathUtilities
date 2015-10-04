using System;
using System.Security;

namespace Goliath.Web.Services
{
    class OnUserPermissionImp : IOnUserPermission
    {
        private readonly IAppUser user;
        //private readonly IDatabaseProvider dbProvider;
        private readonly IPermissionStore permissionStore;
        private IResourceTypeMap resourceTypeMap;

        public OnUserPermissionImp(IAppUser user, IPermissionStore permissionStore, IResourceTypeMap resourceTypeMap)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (resourceTypeMap == null) throw new ArgumentNullException("resourceTypeMap");

            this.user = user;
            this.permissionStore = permissionStore;
            this.resourceTypeMap = resourceTypeMap;
            //this.dbProvider = dbProvider;
        }

        public IUserPermissionProvider On<T>(T entity)
        {
            var type = typeof(T);
            var resourceTypeId = resourceTypeMap.ResolveResourceTypeIdFromType(type);
            if (resourceTypeId < 1)
                throw new SecurityException(string.Format("Resource type Id not found for type {0}", type));

            return new PermissionProvider(user, resourceTypeId, permissionStore) { ResourceName = type.FullName };
        }

        public IUserPermissionProvider OnResourceType(int resourceTypeId, string resourceName)
        {
            return new PermissionProvider(user, resourceTypeId, permissionStore) { ResourceName = resourceName };
        }

        public IUserPermissionProvider OnResourceType(Type resourceType)
        {
            var resourceTypeId = resourceTypeMap.ResolveResourceTypeIdFromType(resourceType);
            if (resourceTypeId < 1)
                throw new SecurityException(string.Format("Resource type Id not found for type {0}", resourceType));

            return new PermissionProvider(user, resourceTypeId, permissionStore) { ResourceName = resourceType.FullName };
        }
    }
}