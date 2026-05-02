using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public interface IClientsRepository
    {
        Task<int> AddNewClientToDb(Client client);
        Task<int> DeleteClientToDb(Client client);
        Task<int> DeleteClientToDb(int clientId);
        Task<int> EditeClientToDb(Client client);
        Task<List<Client>> GetAllClients(string customSql = "");
        Task<List<Client>> GetLoanedClients();
        Task<List<Client>> GetDilayedLoansClients();
        Task<Client> GetClient(string customSql, string executionPart);
        void Dispose();
    }
}