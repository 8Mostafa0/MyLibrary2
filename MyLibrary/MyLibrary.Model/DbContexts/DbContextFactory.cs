using Dapper;
using MyLibrary.ViewModel.Servicies;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MyLibrary.Model.DbContexts
{
    public class DbContextFactory : IDbContextFactory
    {
        #region Dependencies
        private ILogger _logger;
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private Dictionary<string, string> TablesName;
        public string ConnectionString { get; set; }
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public DbContextFactory(string serverName = ".\\Moein", string databaseName = "MyLibrary", string username = "sa", string password = "arta0@")
        {
            FillTablesName();
            ServerName = serverName;
            DatabaseName = databaseName;
            Username = username;
            Password = password;
            ConnectionString = GetConnectionString();
            _logger = LoggerService.logger;
        }
        #endregion

        #region Methods

        private void FillTablesName()
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
                "ClientName NVARCHAR(200) NOT NULL," +
                "BookId INT NOT NULL," +
                "BookName NVARCHAR(200) NOT NULL," +
                "ReturnDate DATETIME2(2) NULL," +
                "ReturnedDate DATETIME2(2) NULL," +
                "CreatedAt DATETIME2(2) NOT NULL," +
                "UpdatedAt DATETIME2(2) NULL);";

            const string ReservedBookSql = "Create Table ReservedBooks" +
                "(Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY," +
                "BookId INT NOT NULL," +
                "BookName NVARCHAR(200) NOT NULL," +
                "ClientId INT NOT NULL," +
                "ClientName NVARCHAR(200) NOT NULL," +
                "CreatedAt Date NOT NULL," +
                "UpdatedAt DATE NOT NULL);";

            TablesName = new Dictionary<string, string>() { { "Clients", ClientsTableSql }, { "Books", BooksTableSql }, { "Loans", LoansTableSql }, { "ReservedBooks", ReservedBookSql } };
        }
        /// <summary>
        /// Get connection string to master database (for creating/dropping databases)
        /// </summary>
        private string GetMasterConnectionString()
        {
            // Same server and credentials, but connect to 'master' database
            string masterConnectionString = $@"data source = {ServerName};" +
                                             $@"initial catalog = master;" +
                                             $@"user id = {Username};" +
                                             $@"password = {Password};" +
                                             @"MultipleActiveResultSets=True;";
            return masterConnectionString;
        }
        /// <summary>
        /// Get a Connection to MyLibrary Database
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string entityConnecitonString = @"" +
                //@"provider = System.Data.SqlClient;" +
                //@"provider connection string = """ +
                $@"data source = {ServerName};" +
                $@"initial catalog = {DatabaseName};" +
                $@"user id = {Username};" +
                $@"password = {Password} ;" +
                @"MultipleActiveResultSets=True;";

            return entityConnecitonString;

        }

        /// <summary>
        /// Get Connection To Database (SQL Server)
        /// </summary>
        /// <returns SqlConnection>sqlconnection to connect database</returns>
        public SqlConnection GetConnection()
        {
            SqlConnection Connection = null;
            try
            {
                Connection = new SqlConnection(ConnectionString);
            }
            catch (SqlException e)
            {
                _logger.Error(e, "DbContext");
            }
            return Connection;
        }
        /// <summary>
        /// Get SqlConnection to master database
        /// </summary>
        private SqlConnection GetMasterConnection()
        {
            SqlConnection Connection = null;
            try
            {
                return new SqlConnection(GetMasterConnectionString());
            }
            catch (SqlException e)
            {
                _logger.Error(e, "DbContext");
            }
            return Connection;
        }
        /// <summary>
        /// execute query for checking table with tableName Exist or not in data base
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private async Task<bool> TableExistsAsync(SqlConnection connection, string tableName)
        {
            string checkTableSql = @"
        SELECT CASE 
            WHEN EXISTS (
                SELECT 1 
                FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = @TableName
            )
            THEN 1
            ELSE 0
        END";

            using (var command = new SqlCommand(checkTableSql, connection))
            {
                command.Parameters.AddWithValue("@TableName", tableName);

                // Returns 1 if exists, 0 if not
                int result = (int)await command.ExecuteScalarAsync();
                return result == 1;
            }
        }

        /// <summary>
        /// Check database and app tables exists if not trying to create them
        /// </summary>
        /// <returns bool>true if database exists</returns>
        public async Task CheckDatabaseExistsAsync()
        {
            using (SqlConnection Connection = GetConnection())
            {

                try
                {
                    await Connection.OpenAsync();
                    bool IsTablesExists = true;
                    foreach (string Name in TablesName.Keys)
                    {
                        bool TablesExist = await TableExistsAsync(Connection, Name);
                        if (!TablesExist)
                        {
                            IsTablesExists = false;
                            break;
                        }
                    }

                    if (!IsTablesExists)
                    {
                        await CleanDatabaseAsync();
                        await CreateDatabaseAsync();
                        await CreateTablesAsync();
                    }

                }
                catch (SqlException e)
                {
                    _logger.Error(e, "CheckDatabaseExistsAsync");
                    await CreateDatabaseAsync();
                }
            }
        }

        /// <summary>
        /// Remove all tables and remove database
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CleanDatabaseAsync()
        {
            string forceDropSql = $@"
                USE master;
                
                -- Kill all connections to the database
                DECLARE @sql NVARCHAR(MAX) = N'';
                SELECT @sql = @sql + 'KILL ' + CAST(session_id AS NVARCHAR(10)) + ';'
                FROM sys.dm_exec_sessions
                WHERE database_id = DB_ID('{DatabaseName}');
                
                EXEC sp_executesql @sql;
                
                -- Set database to SINGLE_USER mode with immediate rollback
                ALTER DATABASE [{DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                
                -- Drop the database
                DROP DATABASE [{DatabaseName}];
            ";
            string RemoveDatabaseSql = $"DROP DATABASE [{DatabaseName}];";
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
            const string LoanTableClientIdForenKeyRule = "ALTER TABLE Loans ADD CONSTRAINT FK_Loans_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id);";
            const string LoanTableBookIdForenKeyRule = "ALTER TABLE Loans ADD CONSTRAINT FK_Loans_Books FOREIGN KEY (BookId) REFERENCES Books(Id);";


            const string ReservedBooksTableBookIdForenKeyRule = "ALTER TABLE ReservedBooks ADD CONSTRAINT FK_Loans_Books FOREIGN KEY (BookId) REFERENCES Books(Id);";
            const string ReservedBooksTableClientIdForenKeyRule = "ALTER TABLE ReservedBooks ADD CONSTRAINT FK_Loans_Clients FOREIGN KEY (ClientId) REFERENCES Clients(Id);";
            List<string> ForionKeys = new List<string>() { LoanTableBookIdForenKeyRule, LoanTableClientIdForenKeyRule, ReservedBooksTableBookIdForenKeyRule, ReservedBooksTableClientIdForenKeyRule };
            using (SqlConnection connection = GetConnection())
            {
                await connection.OpenAsync();
                bool allTablesExists = false;
                foreach (var Name in TablesName.Keys)
                {
                    if (!await TableExistsAsync(connection, Name))
                    {
                        allTablesExists = await connection.ExecuteAsync(TablesName[Name]) > 0;
                    }
                }
                if (!allTablesExists)
                {
                    foreach (var key in ForionKeys)
                    {
                        try
                        {
                            allTablesExists = await connection.ExecuteAsync(key) > 0;
                        }
                        catch (SqlException ex)
                        {
                            allTablesExists = true;
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "Create Tables Forion Keys");
                        }
                    }
                }
                return allTablesExists;
            }
        }

        /// <summary>
        /// create only database only
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CreateDatabaseAsync()
        {
            int ExecuteSqlResult = 0;
            using (SqlConnection connection = GetMasterConnection())
            {
                try
                {
                    connection.Open();
                    string CreateDatabaseSql = $"CREATE  DATABASE [{DatabaseName}];";
                    ExecuteSqlResult = await connection.ExecuteAsync(CreateDatabaseSql);
                }
                catch (SqlException e)
                {
                    _logger.Error(e, "CreateDatabaseAsync");
                }
                finally
                {
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
        public async Task<int> ExecuteQueryAsync(string sqlQuery, string executePart)
        {
            using (SqlConnection Connection = GetConnection())
            {
                Connection.Open();
                try
                {
                    return await Connection.ExecuteAsync(sqlQuery);
                }
                catch (SqlException e)
                {
                    _logger.Error(e, executePart);
                    return -1;
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
