using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;

namespace Goliath.Web.Authorization
{
    public class OAuthOptions : OAuthAuthorizationServerOptions
    {
        public const string SignKey = "YeUR5vQA4KLGgkQWIxyR5pNxZPMEJiXr5WBR6PjJRCxQ8eRuFQZNMPXXbUrqWahL7Ywa";
        public OAuthOptions(IUserAccessManager accessManager)
        {
            TokenEndpointPath = new PathString("/token");
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60);
            AccessTokenFormat = new JwtFormat(this);
            Provider = new OAuthProvider(accessManager);
#if DEBUG
            AllowInsecureHttp = true;
#endif
        }
    }

    public class JwtOptions : JwtBearerAuthenticationOptions
    {
        public JwtOptions()
        {
            var issuer = "localhost";
            var audience = "all";
            var key = Convert.FromBase64String(OAuthOptions.SignKey);

            AllowedAudiences = new[] { audience };
            IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(issuer, key)
            };
        }
    }
}