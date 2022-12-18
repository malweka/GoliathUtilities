using System;
using System.Collections.Generic;
using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Goliath.Utilities.Tests;

[TestClass]
public class SnowflakeGeneratorTests
{
    [TestMethod]
    public void GetNextID_valid_integer()
    {
        var generator = new SnowflakeGenerator();
        var id = generator.GetNextId();
        Console.WriteLine("Id: {0}", id);
        Assert.IsTrue(id.ToString().Length > 12);
    }

    [TestMethod]
    public void GetNextID_with_seed_integer()
    {
        var generator = new SnowflakeGenerator(2020);
        var id = generator.GetNextId();
        Console.WriteLine("Id: {0}", id);
        Assert.IsTrue(id.ToString().Length > 12);
    }

    [TestMethod]
    public  void GetNextID_should_generate_unique_ids()
    {
        var generator = new UniqueLongGenerator();
        var testSet = new HashSet<long>();
        int counter = 0;

        while(counter<1000)
        {
            var id = generator.GetNextId();
            Console.WriteLine("Id: {0}",id);
            testSet.Add(id);
            counter++;
        }
    }

    [TestMethod]
    public void GetNextID_with_seed_should_generate_unique_ids()
    {
        var testSet = new HashSet<long>();
        int counter = 0;

        while (counter < 20)
        {
            var generator = new UniqueLongGenerator(1985+counter);
            var id = generator.GetNextId();
            Console.WriteLine("Id: {0}",id);
            testSet.Add(id);
            counter++;
        }
    }
}