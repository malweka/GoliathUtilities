using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace Goliath.Web
{
    [Serializable]
    public class UserSession : IAppUser, IUserIdentity
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        public IDictionary<string, IDevice> UserDevices { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public IDictionary<string, IRole>  Roles { get; set; }

        internal Services.IPermissionService PermissionService { get; set; }

        /// <summary>
        /// Gets the permission validator.
        /// </summary>
        /// <returns></returns>
        public PermissionValidator GetPermissionValidator()
        {
            return PermissionService == null ? null : new PermissionValidator(PermissionService, this);
        }

        IEnumerable<string> IUserIdentity.Claims
        {
            get
            {
                var claims = new List<string>();
                if (Roles != null && Roles.Count > 0)
                {
                    claims.AddRange(Roles.Values.Select(userRole => userRole.Name));
                }
                return claims;
            }
        }

        string  IUserIdentity.UserName
        {
            get { return UserId; }
        }
    }
}