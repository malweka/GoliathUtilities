namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionBuilder
    {
        /// <summary>
        /// Fors the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IOnUserPermission For(IAppUser user);

    }
}