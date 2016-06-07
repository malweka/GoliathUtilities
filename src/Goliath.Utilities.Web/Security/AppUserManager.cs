using System;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Goliath.Web.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

namespace Goliath.Security
{
    /// <summary>
    /// 
    /// </summary>
    public class GoliathUserManager : UserManager<UserSession, long>
    {
        public GoliathUserManager(IUserStore<UserSession, long> store, IDataProtector protector) : base(store)
        {
            this.UserTokenProvider = new DataProtectorTokenProvider<UserSession, long>(protector);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GoliathSignInManager : SignInManager<UserSession, long>
    {
        public GoliathSignInManager(UserManager<UserSession, long> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(UserSession user)
        {
            var task = new Task<ClaimsIdentity>(() => user.CreateClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie));
            return task;
        }
    }

    public class IdentityUserStore : IUserStore<UserSession, long>
    {
        private readonly IGoliathUserAccessManager accessManager;

        public IdentityUserStore(IGoliathUserAccessManager accessManager)
        {
            this.accessManager = accessManager;
        }

        public void Dispose()
        {
        }

        public Task CreateAsync(UserSession user)
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(UserSession user)
        {
            return Task.FromResult(0);
        }

        public Task DeleteAsync(UserSession user)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<UserSession> FindByIdAsync(long userId)
        {
            Task<UserSession> task = new Task<UserSession>(() =>
            {
                try
                {
                    return accessManager.FindUser(userId);
                }
                catch (Exception ex)
                {
                    throw new SecurityException("Error while trying to find user by ID.", ex);
                }
            });

            return task;
        }

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public Task<UserSession> FindByNameAsync(string userName)
        {
            Task<UserSession> task = new Task<UserSession>(() =>
            {
                try
                {
                    return accessManager.FindUser(userName);
                }
                catch (Exception ex)
                {
                    throw new SecurityException("Error while trying to find user by name.", ex);
                }
            });

            return task;
        }
    }
}