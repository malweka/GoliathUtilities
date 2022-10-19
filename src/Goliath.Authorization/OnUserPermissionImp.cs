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
            var resourceId = secProv.GetResourceGroupIdByType(type);

            if (!resourceId.HasValue)
            {
                logger.LogInformation("Permission evaluator could not determine the resource ID for type {type}. Permission was therefore denied.", type);
                return new NoPermissionFoundEvaluator(user);
            }

            return new PermissionEvaluator(user, resourceId.Value, permissionStore, loggerFactory.CreateLogger<PermissionEvaluator>());
        }

        public IUserPermissionEvaluator OnResource(string resourceName)
        {
            var resourceId = secProv.GetResourceGroupIdByName(resourceName);

            if (!resourceId.HasValue)
            {
                logger.LogInformation("Permission evaluator could not determine the resource ID for resource {resource}. Permission was therefore denied.", resourceName);
                return new NoPermissionFoundEvaluator(user);
            }

            return new PermissionEvaluator(user, resourceId.Value, permissionStore, loggerFactory.CreateLogger<PermissionEvaluator>());
        }

        public IUserPermissionEvaluator OnResource(long resourceId)
        {
            return new PermissionEvaluator(user, resourceId, permissionStore, loggerFactory.CreateLogger<PermissionEvaluator>());
        }

        public IUserPermissionEvaluator OnResourceType(Type resourceType)
        {
            if (resourceType == null) throw new ArgumentNullException(nameof(resourceType));
            var resourceId = secProv.GetResourceGroupIdByType(resourceType);

            if (!resourceId.HasValue)
            {
                logger.LogInformation("Permission evaluator could not determine the resource ID for type {type}. Permission was therefore denied.", resourceType.ToString());
                return new NoPermissionFoundEvaluator(user);
            }

            return new PermissionEvaluator(user, resourceId.Value, permissionStore, loggerFactory.CreateLogger<PermissionEvaluator>());
        }
    }
}