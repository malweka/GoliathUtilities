namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionBuilder : IPermissionBuilder
    {
        private readonly IPermissionStore permissionStore;
        private readonly IResourcePermissionGroupMapper secProv;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionBuilder" /> class.
        /// </summary>
        /// <param name="permissionStore">The permission store.</param>
        /// <param name="secProv">The sec prov.</param>
        public PermissionBuilder(IPermissionStore permissionStore, IResourcePermissionGroupMapper secProv)
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
    }
}