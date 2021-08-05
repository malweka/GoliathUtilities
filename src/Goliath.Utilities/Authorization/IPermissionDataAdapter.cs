using System.Collections.Generic;
using System.Threading.Tasks;
using Goliath.Web;

namespace Goliath.Authorization
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
        IPermissionItem CreateNew(long resourceId, long roleNumber, int permValue);

        /// <summary>
        /// Inserts the specified resource identifier.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <param name="permValue">The perm value.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IPermissionItem Insert(long resourceId, long roleNumber, int permValue, ApplicationContext context = null);

        Task<IPermissionItem> InsertAsync(long resourceId, long roleNumber, int permValue,
            ApplicationContext context = null);

        /// <summary>
        /// Updates the specified permission item.
        /// </summary>
        /// <param name="permissionItem">The permission item.</param>
        /// <param name="context">The context.</param>
        void Update(IPermissionItem permissionItem, ApplicationContext context = null);

        Task UpdateAsync(IPermissionItem permissionItem, ApplicationContext context = null);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        ICollection<IPermissionItem> GetAll();

        Task<ICollection<IPermissionItem>> GetAllAsync();

        /// <summary>
        /// Gets the permission.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <param name="roleNumber">The role number.</param>
        /// <returns></returns>
        IPermissionItem GetPermission(long resourceId, long roleNumber);

        Task<IPermissionItem> GetPermissionAsync(long resourceId, long roleNumber);

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <param name="resourceId">The resource identifier.</param>
        /// <returns></returns>
        PermissionList GetPermissions(long resourceId);

        Task<PermissionList> GetPermissionsAsync(long resourceId);

        /// <summary>
        /// Batches the process.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        /// <param name="updates">The updates.</param>
        /// <param name="deletes">The deletes.</param>
        /// <param name="context">The context.</param>
        void BatchProcess(IList<IPermissionItem> inserts, IList<IPermissionItem> updates, IList<IPermissionItem> deletes,
            ApplicationContext context = null);

        Task BatchProcessAsync(IList<IPermissionItem> inserts, IList<IPermissionItem> updates, IList<IPermissionItem> deletes,
            ApplicationContext context = null);
    }
}