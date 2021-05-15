using System;
using System.Collections.Generic;
using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class SecureRandomTest
    {
        [TestMethod]
        public void Next_with_minimum_and_maximum_value_result_should_be_within_range()
        {
            var random = new SecureRandom();
            int counter = 0;
            while (counter < 20)
            {
                var val = random.Next(12, 300);
                Console.WriteLine("value: {0}", val);
                Assert.IsTrue(val <= 300 && val >= 12);
                counter++;
            }
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Next_with_minimum_and_maximum_min_greater_than_max_should_throw()
        {
            var random = new SecureRandom();
            random.Next(55, 20);
            Assert.Fail("Min cannot be greater than max, should have thrown an out of range exception.");
        }

        [TestMethod]
        public void NextDouble_should_always_return_a_double_between_lesser_than_1()
        {
            var random = new SecureRandom();
            int counter = 0;
            while (counter < 1000)
            {
                var val = random.NextDouble();
                Console.WriteLine("value: {0}", val);
                Assert.IsTrue(val <= 1.0 && val >= 0.0);
                counter++;
            }
        }
    }

    [TestClass]
    public class SaltedSha256HashProviderTests
    {
        [TestMethod]
        public void HashAndVerify()
        {
            var hasher = new SaltedSha256HashProvider();
            string secret = "125f5sfasdfasdfsadfee";
            var h = hasher.ComputeHash(secret.ConvertToByteArray());
            Assert.IsTrue(hasher.VerifyHash(secret.ConvertToByteArray(), h));
        }
    }

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