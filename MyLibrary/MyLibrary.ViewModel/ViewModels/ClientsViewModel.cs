using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.ViewModel.ViewModels
{
    public class ClientsViewModel : ViewModelBase, IClientsViewModel
    {
        #region Dependencies
        private ObservableCollection<IClient> _clients;
        public IEnumerable<IClient> Clients => _clients;

        private IClientsStore _clientsStore;

        private IClient _selectedClient;

        public IClient SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                SelectedClientChanged(value);
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnProperychanged(nameof(FirstName));
            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnProperychanged(nameof(LastName));
            }
        }

        private int _tier;

        public int Tier
        {
            get => _tier;
            set
            {
                _tier = value;
                OnProperychanged(nameof(Tier));
            }
        }

        private string _sortOder;
        public string SortOrder
        {
            get => _sortOder;
            set
            {
                _sortOder = value;
                OnProperychanged(nameof(SortOrder));
            }
        }
        #endregion

        #region Commands
        public IReloadClientsCommand ReloadClientsCommand { get; }
        public ILoadClientsCommand LoadClientsCommand { get; }
        public IDeleteClientCommand DeleteClientCommand { get; }
        public IAddNewClientCommand AddNewClientCommand { get; }
        public IOrderClientsCommand OrderClientsCommand { get; }

        public IEditClientCommand EditClientCommand { get; }

        #endregion

        #region Constructor
        public ClientsViewModel(IClientsStore clientsStore, ILoanRepository loanRepository, IReservedBooksRepository reservedBooksRepository)
        {
            _clients = [];
            _clientsStore = clientsStore;
            _clientsStore.ClientAdded += OnClientAdded;
            _clientsStore.ClientsUpdated += UpdateClients;
            _clientsStore.ClientRemoved += OnClientDeleted;
            _clientsStore.ClientEdited += ClientEdited;
            LoadClientsCommand = new LoadClientsCommand(_clientsStore);
            ReloadClientsCommand = new ReloadClientsCommand(_clientsStore, this);
            DeleteClientCommand = new DeleteClientCommand(this, _clientsStore, loanRepository, reservedBooksRepository);
            AddNewClientCommand = new AddNewClientCommand(this, _clientsStore);
            OrderClientsCommand = new OrderClientsCommand(_clientsStore, this);
            EditClientCommand = new EditClientCommand(this, _clientsStore);
            SortOrder = "0";
        }
        #endregion

        #region Methods
        /// <summary>
        /// called each time client update trigred and update it to clients list
        /// </summary>
        /// <param name="client"></param>
        private void ClientEdited(IClient client)
        {
            int index = _clients.IndexOf(_clients.FirstOrDefault(c => c.ID == client.ID)!);
            if (index >= 0)
            {
                _clients[index] = client;
                CollectionViewSource.GetDefaultView(_clients)?.Refresh();
                MessageBox.Show("کاربر با موفقیت ویرایش شد", "ویرایش کاربر");
            }
        }
        /// <summary>
        /// called each time client delete event trigred and delete it from clients list
        /// </summary>
        /// <param name="client"></param>
        private void OnClientDeleted(IClient client)
        {
            ClearInputs();
            _clients.Remove(client);
            MessageBox.Show("کاربر با موفقیت حذف شد", "حذف کاربر");
        }

        /// <summary>
        /// Clear Inputs data
        /// </summary>
        private void ClearInputs()
        {
            FirstName = "";
            LastName = "";
            Tier = 0;
        }

        /// <summary>
        /// called each time clients list of client store get changed and fill clients list with new values
        /// </summary>
        public void UpdateClients()
        {
            ClearInputs();
            _clients.Clear();
            foreach (IClient client in _clientsStore.Clients)
            {
                _clients.Add(client);
            }
        }
        /// <summary>
        /// called each time add new client event trigred and add it to clients list
        /// </summary>
        /// <param name="client"></param>
        private void OnClientAdded(IClient client)
        {
            ClearInputs();
            client.ID = _clients.Any() ? _clients.Last().ID + 1 : 1;
            _clients.Add(client);
            MessageBox.Show("کاربر با موفقیت اضافه شد", "افزودن کاربر");

        }
        /// <summary>
        /// fill value of first name and last name when select a client
        /// </summary>
        /// <param name="client"></param>
        private void SelectedClientChanged(IClient client)
        {
            if (client != null)
            {
                FirstName = client.FirstName;
                LastName = client.LastName;
                Tier = client.Tier;

            }
        }
        /// <summary>
        /// loader method for clients view model
        /// </summary>
        /// <param name="clientStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <returns></returns>
        public static IClientsViewModel LoadViewModel(IClientsStore clientStore, ILoanRepository loanRepository, IReservedBooksRepository reservedBooksRepository)
        {
            IClientsViewModel ViewModel = new ClientsViewModel(clientStore, loanRepository, reservedBooksRepository);
            ViewModel.LoadClientsCommand.Execute(null);
            return ViewModel;
        }
        #endregion
    }
}
