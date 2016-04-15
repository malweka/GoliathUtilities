using Goliath.Authorization;

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
        /// Gets or sets the permission.
        /// </summary>
        /// <value>
        /// The permission.
        /// </value>
        public PermActionType Permission { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can view.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can view; otherwise, <c>false</c>.
        /// </value>
        public bool CanView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can edit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can edit; otherwise, <c>false</c>.
        /// </value>
        public bool CanEdit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can create.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can create; otherwise, <c>false</c>.
        /// </value>
        public bool CanCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can delete.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can delete; otherwise, <c>false</c>.
        /// </value>
        public bool CanDelete { get; set; }

        /// <summary>
        /// Processes the permission value.
        /// </summary>
        /// <returns></returns>
        public PermActionType ProcessPermissionValue()
        {
            var val = PermActionType.None;

            if (CanCreate)
                val = val | PermActionType.Create;

            if (CanDelete)
                val = val | PermActionType.Delete;

            if (CanEdit)
                val = val | PermActionType.Edit;

            if (CanView)
                val = val | PermActionType.View;

            return val;
        }


        /// <summary>
        /// Creates the specified user role perm.
        /// </summary>
        /// <param name="userRolePerm">The user role perm.</param>
        /// <returns></returns>
        public static PermissionActionModel Create(IUserRolePerm userRolePerm)
        {
            var model = new PermissionActionModel() {RoleNumber = userRolePerm.RoleNumber, ResourceId = userRolePerm.ResourceId, Permission = userRolePerm.PermValue};

            if ((userRolePerm.PermValue & PermActionType.Create) == PermActionType.Create)
                model.CanCreate = true;

            if ((userRolePerm.PermValue & PermActionType.Edit) == PermActionType.Edit)
                model.CanEdit = true;

            if ((userRolePerm.PermValue & PermActionType.Delete) == PermActionType.Delete)
                model.CanDelete = true;

            if ((userRolePerm.PermValue & PermActionType.View) == PermActionType.View)
                model.CanView = true;

            return model;
        }
    }
}