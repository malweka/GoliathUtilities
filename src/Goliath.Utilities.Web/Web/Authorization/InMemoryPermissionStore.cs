using System;
using System.Collections.Generic;
using Goliath.Models;

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

        void RemovePermissionFromCache(int resourceId, int roleId)
        {
            lock (lockPad)
            {
                PermissionList resourcePerms;
                if (permissionCache.TryGetValue(resourceId, out resourcePerms))
                {
                    if (resourcePerms.Contains(roleId))
                    {
                        resourcePerms.Remove(roleId);
                    }
                }
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

        public void UpdateRolePermission(IRole role, IList<PermissionActionModel> permisionModels, ApplicationContext context)
        {
            if (role.Name.Equals("Admin"))
            {
                //admin don't need no permissions
                return;
            }

            List<IPermissionItem> permissionMarkedForDelete = new List<IPermissionItem>();
            List<IPermissionItem> permissionMarkedForUpdate = new List<IPermissionItem>();
            List<IPermissionItem> permissionMarkedForInsert = new List<IPermissionItem>();

            lock (lockPad)
            {
                foreach (var perm in permisionModels)
                {
                    var cachedPerm = GetPermission(perm.ResourceId, role.RoleNumber);
                    var permValue = perm.PermValue;


                    if (cachedPerm != null)
                    {
                        if (permValue == 0)
                        {
                            //should delete
                            permissionMarkedForDelete.Add(cachedPerm);
                            RemovePermissionFromCache(perm.ResourceId, role.RoleNumber);
                        }
                        else
                        {
                            if (permValue != cachedPerm.PermValue)
                            {
                                cachedPerm.PermValue = permValue;
                                permissionMarkedForUpdate.Add(cachedPerm);
                            }
                        }
                    }
                    else
                    {
                        //new permission
                        if (permValue == 0)
                            continue;

                        var newPerm = PermissionDb.CreateNew(perm.ResourceId, role.RoleNumber, permValue);
                        permissionMarkedForInsert.Add(newPerm);
                        CachePermission(newPerm);
                    }
                }
            }

            PermissionDb.BatchProcess(permissionMarkedForInsert, permissionMarkedForUpdate, permissionMarkedForDelete, context);
        }
    }
}