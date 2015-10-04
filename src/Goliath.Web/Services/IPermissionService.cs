namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Fors the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IOnUserPermission For(IAppUser user);

        ///// <summary>
        ///// Fors the specified role.
        ///// </summary>
        ///// <param name="role">The role.</param>
        ///// <returns></returns>
        //IOnRolePermission For(UserRole role);

        ///// <summary>
        ///// Fors the specified user identifier.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns></returns>
        //IOnUserPermission For(string userId);
    }
}