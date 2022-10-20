using System;
using Microsoft.Extensions.Logging;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionBuilder : IPermissionBuilder
    {
        private readonly IPermissionStore permissionStore;
        private readonly IResourcePermissionGroupMapper secProv;
        readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionBuilder" /> class.
        /// </summary>
        /// <param name="permissionStore">The permission store.</param>
        /// <param name="secProv">The sec prov.</param>
        /// <param name="loggerFactory"></param>
        public PermissionBuilder(IPermissionStore permissionStore, IResourcePermissionGroupMapper secProv, ILoggerFactory loggerFactory)
        {
            this.permissionStore = permissionStore ?? throw new ArgumentNullException(nameof(permissionStore));
            this.secProv = secProv ?? throw new ArgumentNullException(nameof(secProv));
            this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        /// <summary>
        /// Check permissions for the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IOnUserPermission For(IAppUser user)
        {
            return new OnUserPermissionImp(user, permissionStore, secProv, loggerFactory);
        }
    }
}