using System;
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
}