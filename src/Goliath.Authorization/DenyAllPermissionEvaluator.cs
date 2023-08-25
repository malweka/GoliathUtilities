using Microsoft.Extensions.Logging;

namespace Goliath.Authorization
{
    class DenyAllPermissionEvaluator : IUserPermissionEvaluator
    {
        public DenyAllPermissionEvaluator(IAppUser user)
        {
            User = user;
        }

        public bool EvaluatePermission(long action)
        {
            return false;
        }

        public IAppUser User { get; }
    }
}