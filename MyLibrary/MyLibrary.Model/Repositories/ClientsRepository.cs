using Dapper;
using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Servicies;
using Serilog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public class ClientsRepository : IClientsRepository
    {
        #region Dependencies
        private readonly IDbContextFactory _dbContextFactory;
        private ILogger _logger;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextFactory"></param>
        /// <param name="logger"></param>
        public ClientsRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _logger = LoggerService.logger;
        }
        #endregion

        #region Method
        /// <summary>
        /// get list of clients from Clients base on sql default get all clients
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns List<Client>></returns>
        public async Task<List<Client>> GetAllClients(string customSql = "")
        {
            List<Client> clients = new List<Client>();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetClientsSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetClientsSQl = "SELECT * FROM Clients";
                    }
                    else
                    {
                        GetClientsSQl = customSql;
                    }
                    clients = Connection.Query<Client>(GetClientsSQl).ToList();
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetAllClients");
                }
                finally
                {
                    Connection.Close();
                }
                return clients;
            }
        }
        /// <summary>
        /// select client using sql that take as input
        /// </summary>
        /// <param name="customSql"></param>
        /// <param name="executionPart"></param>
        /// <returns Client></returns>
        public async Task<Client> GetClient(string customSql, string executionPart)
        {
            Client FetchedClient = new Client();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetClientSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetClientSQl = $"SELECT * FROM Clients LIMIT 1";
                    }
                    else
                    {
                        GetClientSQl = customSql;
                    }
                    FetchedClient = Connection.QuerySingle<Client>(GetClientSQl);
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetAllClients");
                }
                finally
                {
                    Connection.Close();
                }
            }
            return FetchedClient;
        }

        /// <summary>
        ///   add new client to Clients Table
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<int> AddNewClientToDb(Client client)
        {
            string CreateClientSql = $"INSERT INTO Clients(FirstName,LastName,Tier,CreatedAt,UpdatedAt)VALUES(N'{client.FirstName}',N'{client.LastName}','{client.Tier}',GETDATE(),GETDATE())";
            return await _dbContextFactory.ExecuteQueryAsync(CreateClientSql, "AddNewClientToDb");
        }
        /// <summary>
        /// edite client data to Clients Table
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<int> EditeClientToDb(Client client)
        {
            string EditeClientSql = $"UPDATE Clients SET FirstName=N'{client.FirstName}',LastName=N'{client.LastName}',Tier='{client.Tier}',UpdatedAt=GETDATE() WHERE Id='{client.ID}'";
            return await _dbContextFactory.ExecuteQueryAsync(EditeClientSql, "EditeClientToDb");
        }
        /// <summary>
        /// remove client from Client Table by client value
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task<int> DeleteClientToDb(Client client)
        {
            string DeleteClientSql = $"DELETE FROM Clients WHERE Id='{client.ID}'";
            return await _dbContextFactory.ExecuteQueryAsync(DeleteClientSql, "DeleteClientToDb");
        }
        /// <summary>
        /// remove client from Client Table by Client id
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<int> DeleteClientToDb(int clientId)
        {
            string DeleteClientSql = $"DELETE FROM Clients SHERE Id='{clientId}'";
            return await _dbContextFactory.ExecuteQueryAsync(DeleteClientSql, "DeleteClientToDb");
        }

        public void Dispose() { }

        #endregion
    }
}
