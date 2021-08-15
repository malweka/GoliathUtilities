using System;
using System.Collections.Generic;
using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class HmacSha512ProviderTests
    {
        [TestMethod]
        public void VerifyHash()
        {
            string secret = "nDJtbb6dHjJUPhsqHAdWUSS3RO6EDFrL3xO8H6TUGu";
            string val = "Buzz iOS v1.1";
            string hash = "VBh72MY51PXyOeDK34ArJJ4O2yoPOO+Ra4Mpr1YKt1bY0+WWnqyqaUD49ifCN/a7uZ6n4bLi0vz6Nu0tPJprBw==";

            HmacSha512Provider provider = new HmacSha512Provider();
            var v = provider.ComputeHash(secret.ConvertToByteArray(), val.ConvertToByteArray());
            var hv = v.ConvertToBase64String();

            Assert.IsTrue(provider.VerifyHash(secret.ConvertToByteArray(), val.ConvertToByteArray(), hash));
        }

        [TestMethod]
        public void GenerateRandomString()
        {
            var stringGen = new RandomStringGenerator();
            Dictionary<string,int> dictionary = new Dictionary<string, int>();
            int length = 120;
            for (var i = 0; i < 20; i++)
            {
                var key = stringGen.Generate(length, false);
                dictionary.Add(key,i);
                Console.WriteLine(key);
                Assert.AreEqual(length, key.Length);
            }
        }

        [TestMethod]
        public void GenerateSaltedHashes()
        {
            var hasher = new SaltedSha256HashProvider();
            string secret = "ChangeMe123$";
            var b = secret.ConvertToByteArray();
            for (var i = 0; i < 20; i++)
            {
                var key = hasher.ComputeHash(b);
                Console.WriteLine(key.ConvertToBase64String());
            }
        }
    }
}