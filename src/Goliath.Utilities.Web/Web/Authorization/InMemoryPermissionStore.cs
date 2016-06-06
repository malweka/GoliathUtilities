using System;
using System.Collections.Generic;

namespace Goliath.Web.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Goliath.Web.Authorization.BasePermissionStore" />
    public class InMemoryPermissionStore : BasePermissionStore
    {
        static readonly object lockPad = new object();
        static readonly Dictionary<int, PermissionList> permissionCache = new Dictionary<int, PermissionList>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryPermissionStore"/> class.
        /// </summary>
        /// <param name="permissionDataAdapter">The permission data adapter.</param>
        public InMemoryPermissionStore(IPermissionDataAdapter permissionDataAdapter) : base(permissionDataAdapter)
        {

        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public override void Load()
        {
            if (IsLoaded)
                return;

            var permissions = PermissionDb.GetAll();

            lock (lockPad)
            {
                foreach (var permissionItem in permissions)
                {
                    CachePermissionInternal(permissionItem);
                }

                IsLoaded = true;
            }

            
        }

        void CachePermissionInternal(IPermissionItem permission)
        {
            if (permission == null) throw new ArgumentNullException(nameof(permission));

            PermissionList resourcePerms;
            if (permissionCache.TryGetValue(permission.ResourceId, out resourcePerms))
            {
                resourcePerms.Add(permission);
            }
            else
            {
                resourcePerms = new PermissionList { permission };
                permissionCache.Add(permission.ResourceId, resourcePerms);
            }
        }

        protected override void CachePermission(IPermissionItem permission)
        {
            lock (lockPad)
            {
                CachePermissionInternal(permission);
            }
        }

        public override void Reload()
        {
            if (!IsLoaded) return;

            lock (lockPad)
            {
                permissionCache.Clear();
                IsLoaded = false;
            }

            Load();
        }


        public override IPermissionItem GetPermission(int resourceId, int rolenumber)
        {
            VerifyPermissionAreLoaded();

            PermissionList permissionList;

            IPermissionItem permission = null;
            if (permissionCache.TryGetValue(resourceId, out permissionList))
            {
                if (permissionList.Contains(rolenumber))
                    permission = permissionList[rolenumber];
            }
            else
            {
                //permission is not cached let's find it from the database
                permission = PermissionDb.GetPermission(resourceId, rolenumber);
                //cache this permission
                lock (lockPad)
                {
                    CachePermissionInternal(permission);
                }
            }

            return permission;
        }

        public override PermissionList GetPermissions(int resourceId)
        {
            VerifyPermissionAreLoaded();

            PermissionList permissionList;

            if (permissionCache.TryGetValue(resourceId, out permissionList))
            {
                return permissionList;
            }

            //let's try to find permission from the database
            permissionList = PermissionDb.GetPermissions(resourceId);
            //let's cache
            lock (lockPad)
            {
                foreach (var permissionItem in permissionList)
                {
                    CachePermissionInternal(permissionItem);
                }
            }

            return permissionList;
        }
    }
}