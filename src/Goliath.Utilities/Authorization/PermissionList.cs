namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public class PermissionList : System.Collections.ObjectModel.KeyedCollection<long, IPermissionItem>
    {
        /// <summary>
        /// When implemented in a derived class, extracts the key from the specified element.
        /// </summary>
        /// <param name="item">The element from which to extract the key.</param>
        /// <returns>
        /// The key for the specified element.
        /// </returns>
        protected override long GetKeyForItem(IPermissionItem item)
        {
            return item.RoleNumber;
        }
    }
}