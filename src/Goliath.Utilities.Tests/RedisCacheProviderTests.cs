using System;
using System.Collections.Generic;
using Goliath.Caching;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace Goliath.Utilities.Tests
{
    [TestClass]
    public class RedisCacheProviderTests
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;

        static RedisCacheProviderTests()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                KeepAlive = 10,
                AbortOnConnectFail = false,
                ConfigurationChannel = "",
                TieBreaker = "",
                ConfigCheckSeconds = 0,
                //CommandMap = CommandMap.Create(new HashSet<string>
                //{ // EXCLUDE a few commands
                //    "SUBSCRIBE", "UNSUBSCRIBE", "CLUSTER"
                //}, available: false),  
                AllowAdmin = true
            };
            lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        [TestMethod]
        public void Constructor_can_create_provider_with_database()
        {
            var cache = new RedisCacheProvider(lazyConnection.Value.GetDatabase(0));
            Assert.IsNotNull(cache.Database);
        }
    }
}   