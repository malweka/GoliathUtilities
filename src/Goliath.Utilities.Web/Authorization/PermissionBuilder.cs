namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionStore permissionStore;
        private readonly IResourceSecurtityProvider secProv;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionService" /> class.
        /// </summary>
        /// <param name="permissionStore">The permission store.</param>
        public PermissionService(IPermissionStore permissionStore, IResourceSecurtityProvider secProv)
        {
            this.permissionStore = permissionStore;
            this.secProv = secProv;
        }

        /// <summary>
        /// Fors the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IOnUserPermission For(IAppUser user)
        {
            return new OnUserPermissionImp(user, permissionStore, secProv);
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