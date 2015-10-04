using Nancy;
using Nancy.Security;

namespace Goliath.Web.FormAuth
{
    /// <summary>
    ///     Provides a mapping between forms auth guid identifiers and
    ///     real usernames
    /// </summary>
    public interface IUserMapper
    {
        /// <summary>
        ///     Get the real username from an identifier
        /// </summary>
        /// <param name="identifier">User identifier</param>
        /// <param name="context">The current NancyFx context</param>
        /// <returns>Matching populated IUserIdentity object, or empty</returns>
        IUserIdentity GetUserFromIdentifier(string identifier, NancyContext context);
    }
}