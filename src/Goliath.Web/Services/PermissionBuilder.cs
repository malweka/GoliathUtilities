namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionStore permissionStore;
        private readonly IResourceTypeMap resourceTypeMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService" /> class.
        /// </summary>
        /// <param name="permissionStore">The permission store.</param>
        /// <param name="resourceTypeMap">The resource type map.</param>
        public PermissionService(IPermissionStore permissionStore, IResourceTypeMap resourceTypeMap)
        {
            this.permissionStore = permissionStore;
            this.resourceTypeMap = resourceTypeMap;
        }

        /// <summary>
        /// Fors the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IOnUserPermission For(IAppUser user)
        {
            return new OnUserPermissionImp(user, permissionStore, resourceTypeMap);
        }

        ///// <summary>
        ///// Fors the specified role.
        ///// </summary>
        ///// <param name="role">The role.</param>
        ///// <returns></returns>
        //public IOnRolePermission For(UserRole role)
        //{
        //    return new OnRolePermissionImp(role, permissionStore);
        //}

    }
}
