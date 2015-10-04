namespace Goliath.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserPermissionProvider
    {
        /// <summary>
        /// Determines whether this instance [can perform action] the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        bool CanPerformAction(PermActionType action);


        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>
        /// The name of the resource.
        /// </value>
        string ResourceName { get; set; }
    }
}