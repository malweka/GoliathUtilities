using System;
using System.Configuration;
using Goliath.Data.Mapping;
using Goliath.Data.Providers;
using Goliath.Security;

namespace Goliath.Data
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public string ConnectionStringName
        {
            get
            {
                var connect = ConfigurationManager.AppSettings["connectionName"];
                if (string.IsNullOrWhiteSpace(connect))
                    connect = "sqlServer";

                return connect;
            }
        }

        public ISessionFactory SessionFactory
        {
            get
            {
                if (!isInitialized)
                    Init();

                return sessionFactory;
            }
        }

        //private IDbProvider dbProvider;
        private static ISessionFactory sessionFactory;
        private bool isInitialized;

        public void Init(string mapFileName = "data.map.config", string entityNamespace = "Goliath.Data")
        {
            if (isInitialized) return;
            var connString = GetConnectionString();
            var dbProvider = CreateDbProvider(connString.ProviderName);

            var projSettings = new ProjectSettings
            {
                Platform = dbProvider.Name,
                Namespace = entityNamespace,
                ConnectionString = connString.ConnectionString
            };

            var mapFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, mapFileName);

            sessionFactory = new Database()
                .Configure(mapFile, projSettings, null, new GoliathIntegerKeyGenerator())
                .RegisterProvider(dbProvider)
                .Init();


            isInitialized = true;
        }

        IDbProvider CreateDbProvider(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName)) throw new Exception("Connection string provider name was not specified.");

            try
            {
                Type genServType = Type.GetType(providerName);
                if (genServType == null) throw new Exception(string.Format("Could not create provider {0}", providerName));
                var provider = (IDbProvider)Activator.CreateInstance(genServType);
                return provider;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        ConnectionStringSettings GetConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings.Count > 0)
            {
                var connString = ConfigurationManager.ConnectionStrings[ConnectionStringName];
                if (connString != null)
                {
                    return connString;
                }
            }

            throw new Exception(string.Format("No connection string define. Please define a connection string named {0} in the connectionStrings section of your web.config.", ConnectionStringName));
        }
    }
}