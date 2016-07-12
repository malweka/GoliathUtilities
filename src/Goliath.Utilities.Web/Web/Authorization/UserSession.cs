using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Goliath.Web.Authorization
{
    [Serializable]
    public class UserSession : IAppUser, Nancy.Security.IUserIdentity, Microsoft.AspNet.Identity.IUser<long>
    {
        public const string CacheKeyName = "go_ch_sess";

        #region Properties

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
        public string UserName { get; set; }

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

        /// <summary>
        /// Gets or sets the authentication provider.
        /// </summary>
        /// <value>
        /// The authentication provider.
        /// </value>
        public string AuthenticationProvider { get; set; }

        /// <summary>
        /// Gets or sets the profile image URL.
        /// </summary>
        /// <value>
        /// The profile image URL.
        /// </value>
        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public IDictionary<string, IRole> Roles { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets or sets the permission service.
        /// </summary>
        /// <value>
        /// The permission service.
        /// </value>
        public IPermissionBuilder PermissionService { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSession"/> class.
        /// </summary>
        public UserSession()
        {
            Roles = new Dictionary<string, IRole>();
        }

        /// <summary>
        /// Gets the permission validator.
        /// </summary>
        /// <returns></returns>
        public PermissionValidator GetPermissionValidator()
        {
            return PermissionService == null ? null : new PermissionValidator(PermissionService, this);
        }

        IEnumerable<string> Nancy.Security.IUserIdentity.Claims
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

        public ClaimsIdentity CreateClaimsIdentity(string cookieName)
        {
            try
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.GivenName, FirstName),
                    new Claim(ClaimTypes.Surname, LastName),
                    new Claim(ClaimTypes.Email, EmailAddress)};

                if (Roles != null)
                    claims.AddRange(Roles.Select(roleModel => new Claim(ClaimTypes.Role, roleModel.Value.Name)));

                var identity = new ClaimsIdentity(claims, cookieName);
                return identity;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }

        }

        /// <summary>
        /// Determines whether [is in role] [the specified rolename].
        /// </summary>
        /// <param name="rolename">The rolename.</param>
        /// <returns></returns>
        public bool IsInRole(string rolename)
        {
            return Roles.ContainsKey(rolename);
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