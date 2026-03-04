using MyLibrary.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public interface IClientsStore
    {
        IEnumerable<Client> Clients { get; }
        string SearchClientName { get; set; }
        Client SelectedClient { get; set; }

        string SortOrder { get; set; }

        event Action<Client> ClientAdded;
        event Action<Client> ClientEdited;
        event Action<Client> ClientRemoved;
        event Action ClientsUpdated;

        Task AddNewClient(Client client);
        Task DeleteClient(Client client);
        Task EditClient(Client client);
        Task GetOrderedClients(string customSql = "");
        Task Load();
    }
}