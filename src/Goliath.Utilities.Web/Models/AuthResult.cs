using Goliath.Web.Authorization;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public AuthenticationStatus Status { get;  set; }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public UserSession UserSession { get; set; }
    }
}