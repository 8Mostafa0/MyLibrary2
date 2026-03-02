using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public interface IClientsRepository
    {
        Task AddNewClientToDb(Client client);
        Task DeleteClientToDb(Client client);
        Task DeleteClientToDb(int clientId);
        Task EditeClientToDb(Client client);
        Task<List<Client>> GetAllClients(string customSql = "");
        Task<Client> GetClient(string customSql, string executionPart);
    }
}