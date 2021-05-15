using System.Collections.Generic;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppUser
    {
        /// <summary>
        /// Gets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        IDictionary<string, IRole> Roles { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is internal user.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is internal user; otherwise, <c>false</c>.
        /// </value>
        bool IsInternalUser { get; }
    }

}