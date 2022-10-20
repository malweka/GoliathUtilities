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
        public AuthenticationStatus Status { get;  set; }

        public string DisplayMessage { get; set; }

        public string ResultMessage { get; set; }

        public string IpAddress { get; set; }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public UserSession UserSession { get; set; }

        public string Username { get; set; }
    }
}