using System;
using System.Collections.Generic;
using Goliath.Caching;
using Goliath.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;

namespace Goliath.Utilities.Tests
{
#if !INTEGRATION
    // [Ignore]
#endif
    [TestClass]
    public class RedisCacheProviderTests
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection;
        readonly RandomStringGenerator generator = new RandomStringGenerator();

        static RedisCacheProviderTests()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                KeepAlive = 10,
                AbortOnConnectFail = true,
                ConfigurationChannel = "",
                TieBreaker = "",
                ConfigCheckSeconds = 0,
                ConnectTimeout = 12000,
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

        [TestMethod]
        public void Get_should_return_value()
        {
            var key = $"tests:keys:{generator.Generate(6)}";
            var db = lazyConnection.Value.GetDatabase(0);
            db.Set(key, "this is a test", TimeSpan.FromMinutes(5));

            var cache = new RedisCacheProvider(lazyConnection.Value.GetDatabase(0));
            var cachedValue = cache.Get<string>(key);
            Assert.AreEqual("this is a test", cachedValue);
        }

        [TestMethod]
        public void Get_complex_type_should_serialize()
        {
            var key = $"tests:keys:{generator.Generate(6)}";
            var db = lazyConnection.Value.GetDatabase(0);

            var model = new FakeModel { Prop1 = key, Prop2 = 2003234 };
            db.Set(key, model, TimeSpan.FromMinutes(5));

            var cache = new RedisCacheProvider(lazyConnection.Value.GetDatabase(0));
            var cachedValue = cache.Get<FakeModel>(key);

            Assert.AreEqual(model.Prop1, cachedValue.Prop1);
            Assert.AreEqual(model.Prop2, cachedValue.Prop2);
        }

        [TestMethod]
        public void Get_non_existing_should_return_default()
        {
            var key = $"tests:keys:{generator.Generate(6)}";
            var cache = new RedisCacheProvider(lazyConnection.Value.GetDatabase(0));

            var verify = cache.Get<FakeModel>(key);
            Assert.IsNull(verify);
        }

        public class FakeModel
        {
            public string Prop1 { get; set; }
            public int Prop2 { get; set; }
        }
    }
}