using Microsoft.Data.SqlClient;
using MongoDB.Driver;

namespace Dtc.Domain.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection GetSqlConnection();
        IMongoDatabase GetMongoDatabase();
    }
}
