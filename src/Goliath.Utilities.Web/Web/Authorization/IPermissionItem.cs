namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        long Id { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        int ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the role numbeer.
        /// </summary>
        /// <value>
        /// The role numbeer.
        /// </value>
        int RoleNumber { get; set; }

        /// <summary>
        /// Gets or sets the perm value.
        /// </summary>
        /// <value>
        /// The perm value.
        /// </value>
        int PermValue { get; set; }
    }
}