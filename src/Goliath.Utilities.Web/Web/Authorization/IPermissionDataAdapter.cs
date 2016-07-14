using System.Collections.Generic;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionDataAdapter
    {

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="permValue">The perm value.</param>
        /// <returns></returns>
        IPermissionItem CreateNew(int resourceId, int roleNumber, int permValue);

        /// <summary>
        /// Inserts the specified resource identifier.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="permValue">The perm value.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPermissionItem Insert(int resourceId, int roleNumber, int permValue, ApplicationContext context = null);

        /// <summary>
        /// Updates the specified permission item.
        /// </summary>
        /// <param name="permissionItem">The permission item.</param>
        /// <param name="context">The context.</param>
        void Update(IPermissionItem permissionItem, ApplicationContext context = null);

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

        /// <summary>
        /// Batches the process.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        /// <param name="updates">The updates.</param>
        /// <param name="deletes">The deletes.</param>
        /// <param name="context">The context.</param>
        void BatchProcess(IList<IPermissionItem> inserts, IList<IPermissionItem> updates, IList<IPermissionItem> deletes,
            ApplicationContext context = null);
    }
}