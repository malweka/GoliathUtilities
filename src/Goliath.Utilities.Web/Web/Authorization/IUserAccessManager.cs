using Goliath.Models;

namespace Goliath.Web.Authorization
{

    public interface IUserAccessManager<T> : IUserAccessManager
    {
        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserSession FindUser(T userId);


        /// <summary>
        /// Gets the user from cache.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        UserSession GetUserFromCache(T userId);

        /// <summary>
        /// Caches the user.
        /// </summary>
        /// <param name="sessionUser">The session user.</param>
        void CacheUser(UserSession sessionUser);

        /// <summary>
        /// Caches the forget user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        void CacheForgetUser(T userId);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IUserAccessManager
    {
        /// <summary>
        /// Authenticates the external user.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="externalUserId">The external user identifier.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        AuthResult AuthenticateExternalUser(string provider, string externalUserId, string ipAddress);

        /// <summary>
        /// Authenticates the local user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns></returns>
        AuthResult AuthenticateLocalUser(string userName, string password, string ipAddress);

        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        UserSession FindUser(string userName);

    }
}