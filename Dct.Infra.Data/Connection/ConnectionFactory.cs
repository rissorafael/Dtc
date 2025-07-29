using Dct.Infra.CrossCutting.Configuration;
using Dtc.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;

namespace Dct.Infra.Data.Connection
{
    public class ConnectionFactory  : IConnectionFactory
    {
        public SqlConnection GetSqlConnection()
        {
            var connectionString = ValidateSqlServerConnection();

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Não foi possível obter a string de conexão do SQL Server.");

            return new SqlConnection(connectionString);
        }

        public IMongoDatabase GetMongoDatabase()
        {
            var connectionString = ValidateMongoDbeConnection();
            var dbName = ExatractConfiguration.GetMongoDatabaseNameString;

            if (string.IsNullOrEmpty(connectionString))
                throw new Exception("Não foi possível obter a string de conexão do SQL Server.");

            var client = new MongoClient(connectionString);
            return client.GetDatabase(dbName);
        }

        private string ValidateMongoDbeConnection()
        {
            var connectionString = ExatractConfiguration.GetMongoConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                // _logger.LogInformation("Não foi encontrado a variavel de ambiente de conexão com o banco de dados");
                return string.Empty;
            }

            return connectionString;
        }

        private string ValidateSqlServerConnection()
        {
            var connectionString = ExatractConfiguration.GetSqlServerConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                // _logger.LogInformation("Não foi encontrado a variavel de ambiente de conexão com o banco de dados");
                return string.Empty;
            }

            return connectionString;
        }
    }
}
