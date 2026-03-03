using Dapper;
using Serilog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyLibrary.Model.DbContexts
{
    public class DbContextFactory : IDbContextFactory
    {
        #region Dependencies
        private ILogger _logger;
        private string _connectionString;
        private const string _dbName = "MyLibrary";
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DbContextFactory(ILogger logger)
        {
            _connectionString = "Server=localhost;User Id=Mosielite;Password=iFSevr60k7uT;TrustServerCertificate=True;";
            _logger = logger;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get Connection To Database (SQL Server)
        /// </summary>
        /// <returns SqlConnection>sqlconnection to connect database</returns>
        public SqlConnection GetConnection(string databaseName = _dbName, bool withDb = true)
        {
            string StringConnection = _connectionString;
            if (withDb)
            {
                StringConnection += $"Database={databaseName};";
            }
            SqlConnection Connection = null;
            try
            {
                Connection = new SqlConnection(StringConnection);
            }
            catch (SqlException e)
            {
                _logger.Error(e, "DbContext");
            }
            return Connection;
        }


        /// <summary>
        /// Check database and app tables exists if not trying to create them
        /// </summary>
        /// <returns bool>true if database exists</returns>
        public async Task CheckDatabaseExistsAsync()
        {
            bool IsTablesExists = true;
            using (SqlConnection Connection = GetConnection(_dbName))
            {
                try
                {
                    if (Connection == null)
                    {
                        await CreateDatabaseAsync();
                        await CreateTablesAsync();
                        return;
                    }
                    Connection.Open();

                    string CheckDatabaseSql = $"SELECT 1 FROM sys.databases WHERE name = {_dbName};";
                    int SqlExecuteResult = await Connection.ExecuteScalarAsync<int>(CheckDatabaseSql);

                    List<string> TablesNames = new List<string>() { "Clients", "Books", "Loans" };

                    foreach (string Name in TablesNames)
                    {
                        if (!IsTablesExists)
                        {
                            Connection?.Close();
                            await CreateDatabaseAsync();
                            break;
                        }
                        string CheckTableSql = $"SELECT name FROM sys.tables WHERE name ='{Name}'";
                        SqlExecuteResult = await Connection.ExecuteAsync(CheckTableSql);
                        IsTablesExists = SqlExecuteResult > 0;
                    }

                    if (!IsTablesExists)
                    {
                        Connection?.Close();
                        await CleanDatabaseAsync();
                    }

                }
                catch (SqlException e)
                {
                    _logger.Error(e, "CheckDatabaseExistsAsync");
                    await CreateDatabaseAsync();
                }
                finally
                {
                    Connection?.Close();
                }
            }
        }

        /// <summary>
        /// Remove all tables and remove database
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CleanDatabaseAsync()
        {
            string RemoveDatabaseSql = $"ALTER DATABASE [{_dbName}] ; DROP DATABASE [{_dbName}];";
            int SqlExecuteResult = 0;
            using (var Connection = GetConnection())
            {
                try
                {
                    SqlExecuteResult = await Connection.ExecuteAsync(RemoveDatabaseSql);

                    return SqlExecuteResult == -1;

                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "CleanDatabaseAsync");
                }
            }
            return SqlExecuteResult == -1;
        }

        /// <summary>
        /// Create All tables of the app need
        /// </summary>
        private async Task<bool> CreateTablesAsync()
        {
            const string ClientsTableSql = "CREATE TABLE Clients" +
                "(Id INT IDENTITY(1,1) NOT NULL  PRIMARY KEY ," +
                " FirstName  NVARCHAR(100) NOT NULL," +
                "LastName   NVARCHAR(100) NOT NULL," +
                "Tier INT NULL," +
                "CreatedAt  DATETIME2(2) NOT NULL," +
                "UpdatedAt  DATETIME2(2) NULL);";

            const string BooksTableSql = "CREATE TABLE Books" +
                "(Id INT IDENTITY(1,1)   NOT NULL PRIMARY KEY ," +
                "Name NVARCHAR(200) NOT NULL," +
                "Publisher NVARCHAR(150) NULL," +
                "Subject NVARCHAR(100) NULL," +
                "PublicationDate DATE NULL," +
                "Tier INT NOT NULL," +
                "CreatedAt DATETIME2(2) NOT NULL," +
                "UpdatedAt DATETIME2(2) NULL);";

            const string LoansTableSql = "CREATE TABLE Loans" +
                "(Id INT IDENTITY(1,1) NOT NULL   PRIMARY KEY," +
                "ClientId INT NOT NULL," +
                "BookId INT NOT NULL," +
                "ReturnDate DATE NULL," +
                "ReturnedDate Date NULL," +
                "CreatedAt DATETIME2(2) NOT NULL," +
                "UpdatedAt DATETIME2(2) NULL);";

            const string ReservedBookSql = "Create Table ReservedBooks" +
                "(Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY," +
                "BookId INT NOY NULL," +
                "ClientId INT NOT NULL," +
                "CreatedAt Date NOT NULL," +
                "UpdatedAt DATE NOT NYLL);";


            const string LoanTableClientIdForenKeyRule = "ALTER TABLE Loans ADD CONSTRAINT FK_Loans_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id);";
            const string LoanTableBookIdForenKeyRule = "ALTER TABLE Loans ADD CONSTRAINT FK_Loans_Books FOREIGN KEY (BookId) REFERENCES Books(Id);";


            const string ReservedBooksTableBookIdForenKeyRule = "ALTER TABLE ReservedBooks ADD CONSTRAINT FK_Loans_Books FOREIGN KEY (BookId) REFERENCES Books(Id);";
            const string ReservedBooksTableClientIdForenKeyRule = "ALTER TABLE ReservedBooks ADD CONSTRAINT FK_Loans_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id);";

            using (SqlConnection Connection = GetConnection())
            {
                try
                {
                    await Connection.ExecuteAsync(ClientsTableSql);
                    await Connection.ExecuteAsync(BooksTableSql);
                    await Connection.ExecuteAsync(LoansTableSql);
                    await Connection.ExecuteAsync(ReservedBookSql);
                    await Connection.ExecuteAsync(LoanTableBookIdForenKeyRule);
                    await Connection.ExecuteAsync(LoanTableClientIdForenKeyRule);
                    await Connection.ExecuteAsync(ReservedBooksTableBookIdForenKeyRule);
                    await Connection.ExecuteAsync(ReservedBooksTableClientIdForenKeyRule);
                    return true;
                }
                catch (SqlException e)
                {
                    _logger.Error(e, "CreateTablesAsync");
                    return false;
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// create only database only
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CreateDatabaseAsync()
        {
            int ExecuteSqlResult = 0;
            using (SqlConnection Connection = GetConnection("", false))
            {
                Connection.Open();
                try
                {
                    string CreateDatabaseSql = $"CREATE  DATABASE [{_dbName}];";
                    ExecuteSqlResult = await Connection.ExecuteAsync(CreateDatabaseSql);
                    Connection.QuerySingle($"SELECT DB_NAME({_dbName})");
                }
                catch (SqlException e)
                {
                    _logger.Error(e, "CreateDatabaseAsync");
                }
                finally
                {
                    Connection.Close();
                    await CreateTablesAsync();
                }
            }
            return ExecuteSqlResult == 1;

        }
        /// <summary>
        /// Execute custom sqls from repository parts
        /// </summary>
        /// <param name="sqlQuery">custom sql base on schema of the tables</param>
        /// <param name="executePart">the method that call for this methods</param>
        /// <returns></returns>
        public async Task ExecuteQueryAsync(string sqlQuery, string executePart)
        {
            using (SqlConnection Connection = GetConnection(_dbName))
            {
                Connection.Open();
                try
                {
                    await Connection.ExecuteAsync(sqlQuery);
                }
                catch (SqlException e)
                {
                    _logger.Error(e, executePart);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        #endregion
    }
}
