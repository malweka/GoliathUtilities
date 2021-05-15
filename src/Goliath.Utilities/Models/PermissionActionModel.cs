using System.Collections.Generic;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionActionModel
    {
        /// <summary>
        /// Gets or sets the role number.
        /// </summary>
        /// <value>
        /// The role number.
        /// </value>
        public int RoleNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>
        /// The name of the resource.
        /// </value>
        public string ResourceName { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        public int ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the perm value.
        /// </summary>
        /// <value>
        /// The perm value.
        /// </value>
        public int PermValue { get; set; }


        /// <summary>
        /// Gets the permission values.
        /// </summary>
        /// <value>
        /// The permission values.
        /// </value>
        public Dictionary<string, int> PermissionActions { get; } = new Dictionary<string, int>();

        ///// <summary>
        ///// Creates the specified user role perm.
        ///// </summary>
        ///// <param name="userRolePerm">The user role perm.</param>
        ///// <returns></returns>
        //public static PermissionActionModel Create(IUserRolePerm userRolePerm)
        //{
        //    var model = new PermissionActionModel() {RoleNumber = userRolePerm.RoleNumber, ResourceId = userRolePerm.ResourceId, Permission = userRolePerm.PermValue};

        //    if ((userRolePerm.PermValue & PermActionType.Create) == PermActionType.Create)
        //        model.CanCreate = true;

        //    if ((userRolePerm.PermValue & PermActionType.Edit) == PermActionType.Edit)
        //        model.CanEdit = true;

        //    if ((userRolePerm.PermValue & PermActionType.Delete) == PermActionType.Delete)
        //        model.CanDelete = true;

        //    if ((userRolePerm.PermValue & PermActionType.View) == PermActionType.View)
        //        model.CanView = true;

        //    return model;
        //}
    }
}