using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class SaltedSha256HashProviderTests
    {
        [TestMethod]
        public void HashAndVerify_arrays()
        {
            var hasher = new SaltedSha256HashProvider();
            string secret = "125f5sfasdfasdfsadfee";
            var h = hasher.ComputeHash(secret.ConvertToByteArray());
            Assert.IsTrue(hasher.VerifyHash(secret.ConvertToByteArray(), h));
        }

        [TestMethod]
        public void HashAndVerify_strings()
        {
            var hasher = new SaltedSha256HashProvider();
            string secret = "andrada";
            var hash = hasher.ComputeHash(secret);
            Assert.IsTrue(hasher.VerifyHash(secret, hash));
        }
    }

    [TestClass]
    public class Pkdf2HashProviderTests
    {
        [TestMethod]
        public void ComputeHash_should_hash_array()
        {
            var hasher = new Pbkdf2HashProvider();
            var secret = "M'Bem di Fora";
            var saltedHash = hasher.ComputeHash(secret.ConvertToByteArray());
            var value = saltedHash.ConvertToBase64String();
            Assert.IsTrue(hasher.VerifyHash(secret.ConvertToByteArray(), saltedHash));
        }
    }
}