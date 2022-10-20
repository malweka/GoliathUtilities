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

    }
}