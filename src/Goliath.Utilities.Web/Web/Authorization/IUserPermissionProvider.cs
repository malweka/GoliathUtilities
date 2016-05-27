namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserPermissionEvaluator
    {
        /// <summary>
        /// Evaluates the permission.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        bool EvaluatePermission(int action);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        IAppUser User { get; }
    }
}