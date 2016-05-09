using Goliath.Data;
using Goliath.Web.Authorization;

namespace Goliath.Web
{
    public class DefaultContext : ApplicationContext
    {
        public override ISessionFactory SessionFactory { get { return null; } }
        public override UserSession CurrentUser { get { return null; } }
        public override PermissionValidator AuthorizatonValidator { get { return null; } }
    }
}