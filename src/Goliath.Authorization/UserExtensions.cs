using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Goliath.Authorization
{
    public static class UserExtensions
    {
        public static ClaimsIdentity CreateClaimsIdentity(this UserSession user, string authenticationType)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.EmailAddress)};

            if (user.Roles != null)
                claims.AddRange(user.Roles.Select(roleModel => new Claim(ClaimTypes.Role, roleModel.Value.Name)));

            var identity = new ClaimsIdentity(claims, authenticationType);
            return identity;
        }
    }
}