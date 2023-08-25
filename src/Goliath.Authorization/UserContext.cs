using System.Globalization;
using System.Threading;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class UserContext
    {
        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public abstract UserSession CurrentUser { get; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        public virtual CultureInfo Culture => Thread.CurrentThread.CurrentCulture;

        /// <summary>
        /// 
        /// </summary>
        public abstract string SessionId { get; }

        /// <summary>
        /// Gets the permission validator.
        /// </summary>
        /// <value>
        /// The permission validator.
        /// </value>
        public abstract PermissionValidator AuthorizationValidator { get; }

    }
}
