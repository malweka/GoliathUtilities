namespace Goliath.Web
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRolePermission
    {
        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        int ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the role number.
        /// </summary>
        /// <value>
        /// The role number.
        /// </value>
        int RoleNumber { get; set; }

        /// <summary>
        /// Gets or sets the perm value.
        /// </summary>
        /// <value>
        /// The perm value.
        /// </value>
        PermActionType PermValue { get; set; }
    }
}