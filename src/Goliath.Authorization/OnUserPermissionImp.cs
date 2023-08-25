using System;
using Microsoft.Extensions.Logging;

namespace Goliath.Authorization
{
    class OnUserPermissionImp : IOnUserPermission
    {
        private readonly IAppUser user;
        private readonly IResourcePermissionGroupMapper secProv;
        private readonly IPermissionStore permissionStore;
        private readonly ILogger<OnUserPermissionImp> logger;
        private readonly ILoggerFactory loggerFactory;

        public OnUserPermissionImp(IAppUser user, IPermissionStore permissionStore, IResourcePermissionGroupMapper secProv, ILoggerFactory loggerFactory)
        {
            this.user = user ?? throw new ArgumentNullException(nameof(user));
            this.permissionStore = permissionStore ?? throw new ArgumentNullException(nameof(permissionStore));
            this.secProv = secProv ?? throw new ArgumentNullException(nameof(secProv));
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            logger = loggerFactory.CreateLogger<OnUserPermissionImp>();
        }

        public IUserPermissionEvaluator On<T>(T entity)
        {
            var type = typeof(T);
            var resource = secProv.GetResourceDefinition(type);

            if (resource == null)
            {
                logger.LogInformation("Permission evaluator could not determine the resource ID for type {type}. Permission was therefore denied.", type);
                return new DenyAllPermissionEvaluator(user);
            }

            if (resource.Unrestricted)
            {
                logger.LogInformation("Resource {type} mapped to unrestricted resource {resourceId}({resourceName}). Permission will not be enforced", 
                    type, resource.ResourceId, resource.ResourceName);

                return new AllowAllPermissionEvaluator(user);
            }

            return new CompressedPermissionEvaluator(user, resource.ResourceId, permissionStore, loggerFactory.CreateLogger<CompressedPermissionEvaluator>());
        }

        public IUserPermissionEvaluator OnResource(string resourceName)
        {
            var resource = secProv.GetResourceDefinition(resourceName);

            if (resource == null)
            {
                logger.LogInformation("Permission evaluator could not determine the resource ID for resource {resource}. Permission was therefore denied.", resourceName);
                return new DenyAllPermissionEvaluator(user);
            }

            if (resource.Unrestricted)
            {
                logger.LogInformation("Resource {type} mapped to unrestricted resource {resourceId}({resourceName}). Permission will not be enforced",
                    resourceName, resource.ResourceId, resource.ResourceName);

                return new AllowAllPermissionEvaluator(user);
            }

            return new CompressedPermissionEvaluator(user, resource.ResourceId, permissionStore, loggerFactory.CreateLogger<CompressedPermissionEvaluator>());
        }

        public IUserPermissionEvaluator OnResource(long resourceId)
        {
            return new CompressedPermissionEvaluator(user, resourceId, permissionStore, loggerFactory.CreateLogger<CompressedPermissionEvaluator>());
        }

        public IUserPermissionEvaluator OnResourceType(Type resourceType)
        {
            if (resourceType == null) throw new ArgumentNullException(nameof(resourceType));
            var resource = secProv.GetResourceDefinition(resourceType);

            if (resource == null)
            {
                logger.LogInformation("Permission evaluator could not determine the resource ID for type {type}. Permission was therefore denied.", resourceType.ToString());
                return new DenyAllPermissionEvaluator(user);
            }

            if (resource.Unrestricted)
            {
                logger.LogInformation("Resource {type} mapped to unrestricted resource {resourceId}({resourceName}). Permission will not be enforced",
                    resourceType, resource.ResourceId, resource.ResourceName);

                return new AllowAllPermissionEvaluator(user);
            }

            return new CompressedPermissionEvaluator(user, resource.ResourceId, permissionStore, loggerFactory.CreateLogger<CompressedPermissionEvaluator>());
        }
    }
}