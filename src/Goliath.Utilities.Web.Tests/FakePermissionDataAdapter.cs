using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goliath.Web.Authorization;

namespace Goliath.Utilities.Web.Tests
{
    public class FakePermissionDataAdapter : IPermissionDataAdapter
    {
        static readonly Dictionary<int, PermissionList> db = new Dictionary<int, PermissionList>();

        static FakePermissionDataAdapter()
        {
            var res8 = new PermissionList();
            res8.Add(new FakePerm() { Id = 1, ResourceId = 8, RoleNumber = 200, PermValue = 3 });
            res8.Add(new FakePerm() { Id = 2, ResourceId = 8, RoleNumber = 300, PermValue = 1 });
            res8.Add(new FakePerm() { Id = 5, ResourceId = 8, RoleNumber = 400, PermValue = 2 });
            res8.Add(new FakePerm() { Id = 6, ResourceId = 8, RoleNumber = 500, PermValue = 16 });

            db.Add(8, res8);

            var res11 = new PermissionList();
            res11.Add(new FakePerm() { Id = 3, ResourceId = 11, RoleNumber = 200, PermValue = 15 });
            res11.Add(new FakePerm() { Id = 4, ResourceId = 11, RoleNumber = 300, PermValue = 3 });

            db.Add(11, res11);
        }


        private int counter = 14;

        public IPermissionItem Insert(int resourceId, int roleNumber, int permValue)
        {
            PermissionList list;
            var fake = new FakePerm() { Id = counter, ResourceId = resourceId, RoleNumber = roleNumber, PermValue = permValue };
            if (db.TryGetValue(resourceId, out list))
            {
                if (list.Contains(roleNumber))
                {
                    list[roleNumber].PermValue = permValue;
                    return list[roleNumber];
                }
                else
                {
                    counter++;
                    list.Add(fake);
                }
            }
            else
            {
                list = new PermissionList { fake };
                db.Add(resourceId, list);
            }
            return fake;
        }

        public void Update(IPermissionItem permissionItem)
        {
            Insert(permissionItem.ResourceId, permissionItem.RoleNumber, permissionItem.PermValue);
        }

        public ICollection<IPermissionItem> GetAll()
        {
            List<IPermissionItem> perms = new List<IPermissionItem>();
            foreach (var list in db)
            {
                perms.AddRange(list.Value);
            }

            return perms.ToArray();
        }

        public IPermissionItem GetPermission(int resourceId, int roleNumber)
        {
            PermissionList list;
            if (db.TryGetValue(resourceId, out list))
            {
                if (list.Contains(roleNumber))
                {
                    return list[roleNumber];
                }

            }

            return null;
        }

        public PermissionList GetPermissions(int resourceId)
        {
            PermissionList list;
            if (db.TryGetValue(resourceId, out list))
            {
                return list;

            }

            return new PermissionList();
        }
    }

    class FakePerm : IPermissionItem
    {
        public long Id { get; set; }
        public int ResourceId { get; set; }
        public int RoleNumber { get; set; }
        public int PermValue { get; set; }
    }

    public static class PermissionActions
    {
        public const int View = 1;
        public const int Edit = 2;
        public const int Create = 4;
        public const int Delete = 8;
        public const int List = 16;
    }
}
