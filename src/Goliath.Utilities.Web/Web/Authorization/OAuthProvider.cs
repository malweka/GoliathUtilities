using System.Threading.Tasks;
using Goliath.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;

namespace Goliath.Web.Authorization
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserAccessManager accessManager;

        public OAuthProvider(IUserAccessManager accessManager)
        {
            this.accessManager = accessManager;
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (string.IsNullOrWhiteSpace(context.UserName) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.SetError("Incorrect Credentials");
                context.Rejected();
                return Task.FromResult(0);
            }
            var result = accessManager.AuthenticateLocalUser(context.UserName, context.Password, context.ClientId);
            if (((result.Status & AuthenticationStatus.Failed) == AuthenticationStatus.Failed) || (result.UserSession == null))
            {
                context.SetError("Incorrect Credentials");
                context.Rejected();
                return Task.FromResult(0);
            }

            var identity = result.UserSession.CreateClaimsIdentity(DefaultAuthenticationTypes.ExternalBearer); //new ClaimsIdentity[] {};
            context.Validated(identity);
            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                //    var username = context.Parameters["username"];
                //    var password = context.Parameters["password"];

                //    if (username == password)
                //    {
                //        context.OwinContext.Set("otc:username", username);
                //        context.Validated();
                //    }
                //    else
                //    {
                //        context.SetError("Invalid credentials");
                //        context.Rejected();
                //    }
                context.Validated();
            }
            catch
            {
                context.SetError("Server error");
                context.Rejected();
            }

            return Task.FromResult(0);
        }
    }
}