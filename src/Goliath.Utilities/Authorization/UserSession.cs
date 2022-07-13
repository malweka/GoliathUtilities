using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Goliath.Authorization
{
    [Serializable]
    public class UserSession : IAppUser 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public virtual long Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public virtual string UserName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        public virtual UserAccountState Status { get; set; }

        /// <summary>
        /// Gets or sets the authentication provider.
        /// </summary>
        /// <value>
        /// The authentication provider.
        /// </value>
        public virtual string AuthenticationProvider { get; set; }

        /// <summary>
        /// Gets or sets the profile image URL.
        /// </summary>
        /// <value>
        /// The profile image URL.
        /// </value>
        public virtual string ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public IDictionary<string, IRole> Roles { get; set; } = new ConcurrentDictionary<string, IRole>();

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public virtual string FullName => $"{FirstName} {LastName}";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSession"/> class.
        /// </summary>
        public UserSession()
        {
        }

        /// <summary>
        /// Determines whether [is in role] [the specified roleName].
        /// </summary>
        /// <param name="roleName">The roleName.</param>
        /// <returns></returns>
        public bool IsInRole(string roleName)
        {
            return Roles.ContainsKey(roleName);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is internal user.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is internal user; otherwise, <c>false</c>.
        /// </value>
        public bool IsInternalUser => !string.IsNullOrWhiteSpace(ExternalUserId);

        public string ExternalUserId { get; set; }
    }
}