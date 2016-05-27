using System.Collections.Generic;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionDataAdapter
    {
        /// <summary>
        /// Inserts the specified resource identifier.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="permValue">The perm value.</param>
        /// <returns></returns>
        IPermissionItem Insert(int resourceId, int roleNumber, int permValue);

        /// <summary>
        /// Updates the specified permission item.
        /// </summary>
        /// <param name="permissionItem">The permission item.</param>
        void Update(IPermissionItem permissionItem);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        ICollection<IPermissionItem> GetAll();

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <returns></returns>
        IPermissionItem GetPermission(int resourceId, int roleNumber);

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <returns></returns>
        PermissionList GetPermissions(int resourceId);
    }
}