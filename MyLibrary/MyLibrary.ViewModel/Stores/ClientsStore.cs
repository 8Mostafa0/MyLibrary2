using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public class ClientsStore
    {

        #region Dependencies

        private readonly ClientsRepository _clientRepository;
        private List<Client> _clients;

        private readonly Lazy<Task> _initiilizeLazy;


        public IEnumerable<Client> Clients => _clients;

        public event Action<Client> ClientAdded;

        public event Action<Client> ClientEdited;

        public event Action<Client> ClientRemoved;

        public event Action ClientsUpdated;

        public string SearchClientName { get; set; }

        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        public ClientsStore()
        {
            _clientRepository = new ClientsRepository();
            _initiilizeLazy = new Lazy<Task>(Initilize);
            _clients = new List<Client>();
        }
        #endregion


        #region Methods

        public async Task Load()
        {
            await _initiilizeLazy.Value;
        }
        /// <summary>
        /// call for add new client method of database and invoke Client added event
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task AddNewClient(Client client)
        {
            await _clientRepository.AddNewClientToDb(client);
            ClientAdded?.Invoke(client);
        }
        /// <summary>
        /// call for edite method of database and invoke edite client event
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task EditClient(Client client)
        {
            await _clientRepository.EditeClientToDb(client);
            ClientEdited?.Invoke(client);
        }

        /// <summary>
        /// call for delete client method of database and invoke delete event
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task DeleteClient(Client client)
        {
            await _clientRepository.DeleteClientToDb(client);
            ClientRemoved?.Invoke(client);
        }

        /// <summary>
        /// remove all store clients and fill it with new value geted from database
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns></returns>
        public async Task GetOrderedClients(string customSql = "")
        {
            IEnumerable<Client> clients = await _clientRepository.GetAllClients(customSql);
            _clients.Clear();
            _clients.AddRange(clients);
            ClientsUpdated?.Invoke();
        }
        /// <summary>
        /// initilize client list for first time in memory(store)
        /// </summary>
        /// <returns></returns>
        private async Task Initilize()
        {
            IEnumerable<Client> clients = await _clientRepository.GetAllClients();
            _clients.Clear();
            _clients.AddRange(clients);
            ClientsUpdated?.Invoke();
        }
        #endregion
    }
}
