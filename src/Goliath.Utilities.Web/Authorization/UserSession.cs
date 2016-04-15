using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Goliath.Authorization
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

        public string ProfileImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public IDictionary<string, IRole> Roles { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public IPermissionService PermissionService { get; set; }

        #endregion

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

        //internal static UserSession CreateUserSession(User user, IProfileImageManager profileManager, IDictionary<string, RoleModel> roles = null)
        //{
        //    if (user == null) return null;

        //    if (roles == null)
        //        roles = new Dictionary<string, RoleModel>();

        //    var session = new UserSession
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        IsActive = user.IsActive,
        //        Roles = roles,
        //        ProfileImageUrl = profileManager.CreateProfileImageUrl(user)
        //    };


        //    if (user.Contact == null) return session;

        //    session.FirstName = user.Contact.FirstName;
        //    session.LastName = user.Contact.LastName;
        //    session.EmailAddress = user.Contact.EmailAddress;


        //    var stringGenerator = new RandomStringGenerator();
        //    session.SessionId = stringGenerator.Generate(24, false);

        //    return session;
        //}

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
        /// Determines whether this instance is administrator.
        /// </summary>
        /// <returns></returns>
        public bool IsAdministrator()
        {
            return Roles.ContainsKey("Admin");
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
    }
}