using Goliath.Models;

namespace Goliath.Web.Authorization
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
        bool CanPerformAction(int action);


        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>
        /// The name of the resource.
        /// </value>
        string ResourceName { get; set; }
    }

    ///// <summary>
    ///// 
    ///// </summary>
    //public interface IRolePermissionProvider
    //{
    //    /// <summary>
    //    /// Determines whether this instance [can perform action] the specified action.
    //    /// </summary>
    //    /// <param name="action">The action.</param>
    //    /// <returns></returns>
    //    bool CanPerformAction(PermActionType action);

    //    ///// <summary>
    //    ///// Adds the or modify permission.
    //    ///// </summary>
    //    ///// <param name="action">The action.</param>
    //    //void AddOrModifyPermission(PermActionType action);

    //    /// <summary>
    //    /// Removes the permission.
    //    /// </summary>
    //    void RemovePermission();


    //    /// <summary>
    //    /// Gets or sets the name of the resource.
    //    /// </summary>
    //    /// <value>
    //    /// The name of the resource.
    //    /// </value>
    //    string ResourceName { get; set; }
    //}
}