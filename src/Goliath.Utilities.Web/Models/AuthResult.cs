using Goliath.Authorization;

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
        public AuthenticationStatus Status { get; internal set; }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public UserSession UserSession { get; internal set; }
    }
}