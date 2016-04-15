using Autofac;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace Goliath.Web
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Uses the nancy data protector provider.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        /// <param name="container">The container.</param>
        public static void UseNancyDataProtectorProvider(this IAppBuilder appBuilder, IContainer container)
        {
            var provider = container.Resolve<IDataProtectionProvider>();
            appBuilder.SetDataProtectionProvider(provider);
        }
    }
}