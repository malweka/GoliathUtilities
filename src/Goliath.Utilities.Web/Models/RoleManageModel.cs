using Goliath.Web.Authorization;

namespace Goliath.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleManageModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public ListViewModel<UserSession> Users { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public ListViewModel<PermissionActionModel> Permissions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleManageModel"/> class.
        /// </summary>
        public RoleManageModel()
        {
            Users = new ListViewModel<UserSession>();
            Permissions = new ListViewModel<PermissionActionModel>();
        }
    }
}