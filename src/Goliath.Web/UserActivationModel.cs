namespace Goliath.Web
{
    public class UserActivationModel
    {
        public long UserId { get; set; }
        public long TokenId { get; set; }
        public string TokenHash { get; set; }
        public string Password { get; set; }
    }

    public class PermissionActionModel
    {

        public int RoleNumber { get; set; }

        public string ResourceName { get; set; }

        public int ResourceId { get; set; }

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


        public static PermissionActionModel Create(IUserRolePermission userRolePerm)
        {
            var model = new PermissionActionModel()
            {
                RoleNumber = userRolePerm.RoleNumber,
                ResourceId = userRolePerm.ResourceId,
                Permission = userRolePerm.PermValue
            };

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