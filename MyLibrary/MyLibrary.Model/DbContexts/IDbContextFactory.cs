using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyLibrary.Model.DbContexts
{
    public interface IDbContextFactory
    {
        Task CheckDatabaseExistsAsync();
        Task ExecuteQueryAsync(string sqlQuery, string executePart);
        SqlConnection GetConnection(string databaseName = "MyLibrary", bool withDb = true);
    }
}