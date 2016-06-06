using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goliath.Web.Authorization;
using NUnit.Framework;

namespace Goliath.Utilities.Web.Tests
{
    [TestFixture]
    public class InMemoryPermissionStoreTests
    {
        [Test]
        public void Load_should_load_all_permissions_in_memory()
        {
            var dbAdapter = new FakePermissionDataAdapter();
            var permissionStore = new InMemoryPermissionStore(dbAdapter);
            permissionStore.Load();

            var perm = (FakePerm) permissionStore.GetPermission(8, 200);
            Assert.AreEqual(1, perm.Id);
        }

        [Test]
        public void AddPermission_for_an_existing_permission_will_perform_binary_or_on_permvalue()
        {
            var dbAdapter = new FakePermissionDataAdapter();
            var permissionStore = new InMemoryPermissionStore(dbAdapter);
            permissionStore.Load();

            //try to add edit permission. this role only has view 
            permissionStore.AddPermission(8,300, PermissionActions.Edit);
            var perm = permissionStore.GetPermission(8, 300);
            Assert.AreEqual(3, perm.PermValue);
        }

        [Test]
        public void AddPermission_for_an_existing_permission_try_to_add_existing_permission_value_should_not_update()
        {
            var dbAdapter = new FakePermissionDataAdapter();
            var permissionStore = new InMemoryPermissionStore(dbAdapter);
            permissionStore.Load();

            //try to add edit permission. this role only has Edit 
            permissionStore.AddPermission(8, 400, PermissionActions.Edit);
            var perm = permissionStore.GetPermission(8, 400);
            Assert.AreEqual(PermissionActions.Edit, perm.PermValue);
        }

        [Test]
        public void AddPermission_add_new_permission_it_should_be_created_and_cached()
        {
            var dbAdapter = new FakePermissionDataAdapter();
            var permissionStore = new InMemoryPermissionStore(dbAdapter);
            permissionStore.Load();

            //try to add edit permission. this role only has Edit 
            permissionStore.AddPermission(8, 401, PermissionActions.Edit);
            var perm = permissionStore.GetPermission(8, 401);
            Assert.AreEqual(PermissionActions.Edit, perm.PermValue);
        }

        [Test]
        public void RemovePermission_existing_permissions()
        {
            var dbAdapter = new FakePermissionDataAdapter();
            var permissionStore = new InMemoryPermissionStore(dbAdapter);
            permissionStore.Load();

            permissionStore.RemovePermission(11, 200, PermissionActions.Edit);
            var perm = permissionStore.GetPermission(11, 200);
            Assert.AreEqual(13, perm.PermValue);
        }
    }
}
