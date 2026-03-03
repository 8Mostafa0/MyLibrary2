using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public class ClientsViewModel : ViewModelBase, IClientsViewModel
    {
        #region Dependencies
        private ObservableCollection<Client> _clients;
        public IEnumerable<Client> Clients => _clients;

        private IClientsStore _clientsStore;

        private Client _selectedClient;

        private IMessageBoxStore _messageBoxStore;
        public bool IsMessageBoxOpen => _messageBoxStore.IsMessageOpen;
        public IViewModelBase CurrentMessageBox => _messageBoxStore.MessageBoxViewModel;
        public Client SelectedClient
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

        public ICommand EditClientCommand { get; }

        #endregion

        #region Constructor
        public ClientsViewModel()
        {
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _messageBoxStore.MessageViewModelChanged += OnMessageBoxChanged;
            _clients = new ObservableCollection<Client>();
            _clientsStore = ClassFactory.CreateClientsStore();
            _clientsStore.ClientAdded += OnClientAdded;
            _clientsStore.ClientsUpdated += UpdateClients;
            _clientsStore.ClientRemoved += OnClientDeleted;
            _clientsStore.ClientEdited += ClientEdited;
            LoadClientsCommand = ClassFactory.CreateLoadClientsCommand();
            ReloadClientsCommand = ClassFactory.CreateReloadClientsCommand();
            DeleteClientCommand = ClassFactory.CreateDeleteClientCommand();
            AddNewClientCommand = ClassFactory.CreateAddNewClientCommand();
            OrderClientsCommand = ClassFactory.CreateOrderClientsCommand();
            EditClientCommand = new EditClientCommand(this, _clientsStore, _messageBoxStore);
            SortOrder = "0";
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
        }
        #endregion

        #region Methods

        /// <summary>
        /// get called each tim change value of Messagebox Changed event trigred
        /// </summary>
        private void OnMessageBoxChanged()
        {

            OnProperychanged(nameof(CurrentMessageBox));
            OnProperychanged(nameof(IsMessageBoxOpen));
        }

        /// <summary>
        /// called each time client update trigred and update it to clients list
        /// </summary>
        /// <param name="client"></param>
        private void ClientEdited(Client client)
        {
            int index = _clients.IndexOf(_clients.FirstOrDefault(c => c.ID == client.ID));
            if (index >= 0)
            {
                _clients.RemoveAt(index);
                _clients.Add(client);
                int newIndex = _clients.IndexOf(_clients.FirstOrDefault(c => c.ID == client.ID));
                _clients.Move(newIndex, index);
                _messageBoxStore.Show("کاربر با موفقیت ویرایش شد", "ویرایش کاربر");
            }
        }
        /// <summary>
        /// called each time client delete event trigred and delete it from clients list
        /// </summary>
        /// <param name="client"></param>
        private void OnClientDeleted(Client client)
        {
            ClearInputs();
            _clients.Remove(client);
            _messageBoxStore.Show("کاربر با موفقیت حذف شد", "حذف کاربر");
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
            foreach (Client client in _clientsStore.Clients)
            {
                _clients.Add(client);
            }
        }
        /// <summary>
        /// called each time add new client event trigred and add it to clients list
        /// </summary>
        /// <param name="client"></param>
        private void OnClientAdded(Client client)
        {
            ClearInputs();
            client.ID = _clients.Any() ? _clients.Last().ID + 1 : 1;
            _clients.Add(client);
            _messageBoxStore.Show("کاربر با موفقیت اضافه شد", "افزودن کاربر");

        }
        /// <summary>
        /// fill value of first name and last name when select a client
        /// </summary>
        /// <param name="client"></param>
        private void SelectedClientChanged(Client client)
        {
            if (client != null)
            {
                FirstName = client.FirstName;
                LastName = client.LastName;
                Tier = client.Tier;

            }
        }
        #endregion

    }
}
