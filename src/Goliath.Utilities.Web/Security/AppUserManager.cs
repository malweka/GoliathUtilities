using System.Security.Claims;
using System.Threading.Tasks;
using Goliath.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;

namespace Goliath.Security
{
    ///// <summary>
    ///// 
    ///// </summary>
    //public class SchedulerUserManager : UserManager<UserSession, long>
    //{
    //    public SchedulerUserManager(IUserStore<UserSession, long> store, IDataProtector protector) : base(store)
    //    {
    //        this.UserTokenProvider = new DataProtectorTokenProvider<UserSession, long>(protector);
    //    }

    //    public override Task<bool> CheckPasswordAsync(UserSession user, string password)
    //    {
    //        return base.CheckPasswordAsync(user, password);
    //    }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //public class SchedulerSignInManager : SignInManager<UserSession, long>
    //{
    //    public SchedulerSignInManager(UserManager<UserSession, long> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
    //    {
    //    }

    //    public override Task<ClaimsIdentity> CreateUserIdentityAsync(UserSession user)
    //    {
    //        var task = new Task<ClaimsIdentity>(() => user.CreateClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie));
    //        return task;
    //    }
    //}

    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your email service here to send an email.
    //        return Task.FromResult(0);
    //    }
    //}

    //public class SmsService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your SMS service here to send a text message.
    //        return Task.FromResult(0);
    //    }
    //}
}