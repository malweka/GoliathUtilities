using System;
using Goliath.Models;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionValidator
    {
        private readonly IPermissionService permissionService;
        private readonly IAppUser user;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionValidator"/> class.
        /// </summary>
        /// <param name="permissionService">The permission service.</param>
        /// <param name="user">The user.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentNullException">
        /// permissionService
        /// or
        /// user
        /// </exception>
        public PermissionValidator(IPermissionService permissionService, IAppUser user)
        {
            if (permissionService == null) throw new ArgumentNullException(nameof(permissionService));
            if (user == null) throw new ArgumentNullException(nameof(user));

            this.permissionService = permissionService;
            this.user = user;
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public bool CanPerformAction(int resourceTypeId, int action)
        {
            return permissionService.For(user)
                .OnResourceType(resourceTypeId, string.Empty)
                .CanPerformAction(action);
        }

        /// <summary>
        /// Determines whether this instance [can perform action] the specified resource type.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public bool CanPerformAction(Type resourceType, int action)
        {
            return permissionService.For(user)
                .OnResourceType(resourceType)
                .CanPerformAction(action);
        }

        /// <summary>
        /// Determines whether this instance can view the specified resource type.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        public bool CanView(Type resourceType)
        {
            return permissionService.For(user).OnResourceType(resourceType)
                .CanPerformAction(DefaultPermissions.View);
        }

        /// <summary>
        /// Determines whether this instance can view the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <returns></returns>
        public bool CanView(int resourceTypeId)
        {
            return permissionService.For(user).OnResourceType(resourceTypeId, string.Empty)
                .CanPerformAction(DefaultPermissions.View);
        }

        /// <summary>
        /// Determines whether this instance can edit the specified resource type.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        public bool CanEdit(Type resourceType)
        {
            return permissionService.For(user).OnResourceType(resourceType)
                .CanPerformAction(DefaultPermissions.Edit);
        }

        /// <summary>
        /// Determines whether this instance can edit the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <returns></returns>
        public bool CanEdit(int resourceTypeId)
        {
            return permissionService.For(user).OnResourceType(resourceTypeId, string.Empty)
                .CanPerformAction(DefaultPermissions.Edit);
        }

        /// <summary>
        /// Determines whether this instance can create the specified resource type.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        public bool CanCreate(Type resourceType)
        {
            return permissionService.For(user).OnResourceType(resourceType)
                .CanPerformAction(DefaultPermissions.Create);
        }

        /// <summary>
        /// Determines whether this instance can create the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <returns></returns>
        public bool CanCreate(int resourceTypeId)
        {
            return permissionService.For(user).OnResourceType(resourceTypeId, string.Empty)
                .CanPerformAction(DefaultPermissions.Create);
        }

        /// <summary>
        /// Determines whether this instance can delete the specified resource type.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        public bool CanDelete(Type resourceType)
        {
            return permissionService.For(user).OnResourceType(resourceType)
                .CanPerformAction(DefaultPermissions.Delete);
        }

        /// <summary>
        /// Determines whether this instance can delete the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <returns></returns>
        public bool CanDelete(int resourceTypeId)
        {
            return permissionService.For(user).OnResourceType(resourceTypeId, string.Empty)
                .CanPerformAction(DefaultPermissions.Delete);
        }

        /// <summary>
        /// Determines whether this instance can list the specified resource type.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <returns></returns>
        public bool CanList(Type resourceType)
        {
            return permissionService.For(user).OnResourceType(resourceType)
                .CanPerformAction(DefaultPermissions.List);
        }

        /// <summary>
        /// Determines whether this instance can list the specified resource type identifier.
        /// </summary>
        /// <param name="resourceTypeId">The resource type identifier.</param>
        /// <returns></returns>
        public bool CanList(int resourceTypeId)
        {
            return permissionService.For(user).OnResourceType(resourceTypeId, string.Empty)
                .CanPerformAction(DefaultPermissions.List);
        }

        public static class DefaultPermissions
        {
            public const int View = 1;
            public const int Edit = 2;
            public const int Create = 4;
            public const int Delete = 8;
            public const int List = 16;
        }
    }
}