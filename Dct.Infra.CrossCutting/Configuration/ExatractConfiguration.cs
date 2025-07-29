using Microsoft.Extensions.Configuration;

namespace Dct.Infra.CrossCutting.Configuration
{
    public static class ExatractConfiguration
    {
        //public static string GetSqlServerConnectionString =>
        //    Environment.GetEnvironmentVariable("SQLSERVER_CONNECTIONSTRING")
        //    ?? throw new Exception("SQL Server connection string not found.");

        //public static string GetMongoConnectionString =>
        //    Environment.GetEnvironmentVariable("MONGODB_CONNECTIONSTRING")
        //    ?? "mongodb://localhost:27017";

        //public static string GetMongoDatabaseName =>
        //    Environment.GetEnvironmentVariable("MONGODB_DATABASE")
        //    ?? "ProductDb";


        static IConfiguration Config;

        public static void Initialize(IConfiguration configuration)
        {
            Config = configuration;
        }

        public static string GetMongoConnectionString
        {
            get
            {
                return GetConnectionMongoDb();
            }
        }

        public static string GetSqlServerConnectionString
        {
            get
            {
                return GetConnectionSqlServer();
            }
        }

        public static string GetMongoDatabaseNameString
        {
            get
            {
                return GetMongoDatabaseName();
            }
        }

        private static string GetConnectionSqlServer()
        {
            var connectionString = Config.GetConnectionString("SqlServerConnection");
            return connectionString;
        }
        private static string GetConnectionMongoDb()
        {
            var connectionString = Config.GetConnectionString("MongoDB");
            return connectionString;
        }

        private static string GetMongoDatabaseName()
        {
            var connectionString = Config.GetSection("MongoDB:DatabaseName").Value;
            return connectionString;
        }
    }
}
