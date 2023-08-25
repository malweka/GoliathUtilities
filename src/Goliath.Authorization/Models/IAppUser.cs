using System.Collections.Generic;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppUser
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        string Username { get; set; }

        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        IDictionary<string, IRole> Roles { get; }
    }

}