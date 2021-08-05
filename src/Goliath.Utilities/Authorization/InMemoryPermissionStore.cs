using System;
using System.Collections.Generic;
using Goliath.Models;
using Goliath.Web;

namespace Goliath.Authorization
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BasePermissionStore" />
    public class InMemoryPermissionStore : BasePermissionStore
    {
        static readonly object lockPad = new object();
        static readonly Dictionary<long, PermissionList> permissionCache = new Dictionary<long, PermissionList>();

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

        void RemovePermissionFromCache(long resourceId, long roleId)
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


        public override IPermissionItem GetPermission(long resourceId, long roleNumber)
        {
            VerifyPermissionAreLoaded();

            PermissionList permissionList;

            IPermissionItem permission = null;
            if (permissionCache.TryGetValue(resourceId, out permissionList))
            {
                if (permissionList.Contains(roleNumber))
                    permission = permissionList[roleNumber];
            }
            else
            {
                //permission is not cached let's find it from the database
                permission = PermissionDb.GetPermission(resourceId, roleNumber);
                if (permission == null)
                {
                    //permission does not exist in the database either. probably is a new permission.
                    return null;
                }

                //cache this permission
                lock (lockPad)
                {
                    CachePermissionInternal(permission);
                }
            }

            return permission;
        }

        public override PermissionList GetPermissions(long resourceId)
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

        public override void UpdateRolePermissions(IRole role, IList<PermissionActionModel> permissionModels, ApplicationContext context = null)
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
                foreach (var perm in permissionModels)
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