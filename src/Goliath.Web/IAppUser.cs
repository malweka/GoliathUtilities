using System.Collections.Generic;

namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppUser
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        long Id { get; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        string UserId { get; }

        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        IDictionary<string, IRole> Roles { get; }

    }
}