using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class HmacSha256ProviderTests
    {
        [TestMethod]
        public void VerifyHash()
        {
            string secret = "nDJtbb6dHjJUPhsqHAdWUSS3RO6EDFrL3xO8H6TUGu";
            string val = "Buzz iOS v1.1";
            string hash = "vL4sObQQyhovIDUuiD3bZNUzXvRjIFGR0zliK1jImcs=";

            HmacSha256Provider provider = new HmacSha256Provider();
            var v = provider.ComputeHash(secret.ConvertToByteArray(), val.ConvertToByteArray());
            var hv = v.ConvertToBase64String();

            Assert.IsTrue(provider.VerifyHash(secret.ConvertToByteArray(), val.ConvertToByteArray(), hash ));
        }
    }
}