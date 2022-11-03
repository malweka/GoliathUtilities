namespace Goliath.Authorization
{
    class AllowAllPermissionEvaluator : IUserPermissionEvaluator
    {

        public AllowAllPermissionEvaluator(IAppUser user)
        {
            User = user;
        }

        public bool EvaluatePermission(int action)
        {
            return true;
        }


        public IAppUser User { get; }
    }
}