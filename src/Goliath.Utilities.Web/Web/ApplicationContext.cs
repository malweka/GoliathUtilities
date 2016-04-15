using System.Globalization;
using Goliath.Authorization;
using Goliath.Data;

namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ApplicationContext
    {
        /// <summary>
        /// Gets or sets the session factory.
        /// </summary>
        /// <value>
        /// The session factory.
        /// </value>
        public abstract ISessionFactory SessionFactory { get; }

        /// <summary>
        /// Gets or sets the current site URL.
        /// </summary>
        /// <value>
        /// The current site URL.
        /// </value>
        public virtual string CurrentSiteUrl { get; set; }

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
        public CultureInfo Culture { get; protected set; }

        /// <summary>
        /// Gets the permission validator.
        /// </summary>
        /// <value>
        /// The permission validator.
        /// </value>
        public abstract PermissionValidator AuthorizatonValidator { get; }

    }
}
