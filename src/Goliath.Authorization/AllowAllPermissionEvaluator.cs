namespace Goliath.Authorization
{
    class AllowAllPermissionEvaluator : IUserPermissionEvaluator
    {

        public AllowAllPermissionEvaluator(IAppUser user)
        {
            User = user;
        }

        public bool EvaluatePermission(long action)
        {
            return true;
        }


        public IAppUser User { get; }
    }
}