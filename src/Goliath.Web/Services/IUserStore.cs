namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserStore
    {
        /// <summary>
        /// Gets the application user.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <returns></returns>
        UserSession GetAppUser(string sessionId);

        /// <summary>
        /// Caches the user session.
        /// </summary>
        /// <param name="user">The user.</param>
        void CacheUserSession(UserSession user);
    }
}