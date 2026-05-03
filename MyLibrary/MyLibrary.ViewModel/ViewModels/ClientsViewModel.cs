using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels
{
    public class ClientsViewModel : PropertyChangedBase, IClientsViewModel
    {
        #region Dependencies

        private Client _selectedClient;
        private IMessageBoxStore _messageBoxStore;
        private IMyLibraryDbContext _db;
        private ObservableCollection<Client> _clients;
        public bool IsMessageBoxOpen => _messageBoxStore.IsMessageOpen;
        public IViewModelBase CurrentMessageBox => _messageBoxStore.MessageBoxViewModel;
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                SetField(ref _clients, value);
            }
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                SetField(ref _selectedClient, value);
                OnClientDataChanged();
                SelectedClientChanged(value);
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                SetField(ref _firstName, value);
                OnClientDataChanged();
            }
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                SetField(ref _lastName, value);
                OnClientDataChanged();
            }
        }

        private int _tier;

        public int Tier
        {
            get => _tier;
            set
            {
                SetField(ref _tier, value);
                OnClientDataChanged();
            }
        }

        private int _sortOder;
        public int SortOrder
        {
            get => _sortOder;
            set
            {
                SetField(ref _sortOder, value);
            }
        }
        #endregion

        #region Commands
        private AsyncRelayCommand _reloadClientsCommand;
        public AsyncRelayCommand ReloadClientsCommand => _reloadClientsCommand ?? (_reloadClientsCommand = new AsyncRelayCommand(RefreshPage));
        public AsyncRelayCommand DeleteClientCommand { get; }
        public AsyncRelayCommand AddNewClientCommand { get; }
        public AsyncRelayCommand OrderClientsCommand { get; }

        public AsyncRelayCommand EditClientCommand { get; }

        ObservableCollection<Client> IClientsViewModel._clients => throw new System.NotImplementedException();

        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageBoxStore"></param>
        /// <param name="db"></param>
        public ClientsViewModel(
            IMessageBoxStore messageBoxStore,
            IMyLibraryDbContext db)
        {
            _messageBoxStore = messageBoxStore;
            _db = db;
            Clients = new ObservableCollection<Client>();

            AddNewClientCommand = new AsyncRelayCommand(AddNewClient, ValidateInputData);
            EditClientCommand = new AsyncRelayCommand(ClientEdited, () => { return ValidateInputData() && !(SelectedClient is null); });
            DeleteClientCommand = new AsyncRelayCommand(DelecteClient, () => { return !(SelectedClient is null); });
            OrderClientsCommand = new AsyncRelayCommand(SortCLientsList);
            SortOrder = 0;
            _messageBoxStore.MessageViewModelChanged += OnMessageBoxChanged;
            RefreshPage();
        }
        #endregion

        #region Methods

        /// <summary>
        /// sort clients list base on SortOrder
        /// </summary>
        /// <returns></returns>
        private async Task SortCLientsList()
        {
            List<Client> clients = new List<Client>();
            Clients.Clear();
            switch (SortOrder)
            {
                case 0: clients = await _db.ClientsRepository.GetAllClients(); break;
                case 1: clients = await _db.ClientsRepository.GetLoanedClients(); break;
                case 2: clients = await _db.ClientsRepository.GetDilayedLoansClients(); break;
            }
            foreach (Client client in clients)
            {
                Clients.Add(client);
            }
            ClearInputs();

        }

        /// <summary>
        /// get called each tim change value of Messagebox Changed event trigred
        /// </summary>
        private void OnMessageBoxChanged()
        {
            OnPropertyChanged(nameof(CurrentMessageBox));
            OnPropertyChanged(nameof(IsMessageBoxOpen));
        }

        /// <summary>
        /// validate silently inputed data
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputData()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// triger event for commands to check can exceute commands or not
        /// </summary>
        private void OnClientDataChanged()
        {

            AddNewClientCommand.RaiseCanExecuteChanged();
            EditClientCommand.RaiseCanExecuteChanged();
            DeleteClientCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// try to edit client in database
        /// </summary>
        private async Task ClientEdited()
        {
            Client client = SelectedClient;
            if (SelectedClient is null)
            {
                _messageBoxStore.Show("لطفا ابتدا کاربری را برای ویرایش انتخاب کنید", "ویرایش کاربر");
                return;
            }
            client.FirstName = FirstName;
            client.LastName = LastName;
            client.Tier = Tier;
            int result = await _db.ClientsRepository.EditeClientToDb(client);
            if (result > 0)
            {
                _messageBoxStore.Show("کاربر با موفقیت ویرایش شد", "ویرایش کاربر");
                RefreshPage();
            }
            else
            {
                _messageBoxStore.Show("هنگام ویرایش اطلاعات کاربر مشکلی بوجود امده است", "ویرایش کاربر");
            }
        }

        /// <summary>
        /// delete selected client from database
        /// </summary>
        /// <returns></returns>
        private async Task DelecteClient()
        {
            int result = await _db.ClientsRepository.DeleteClientToDb(SelectedClient);
            if (result > 0)
            {
                _messageBoxStore.Show("حذف کاربر با موفقیت انجام شد", "حذف کاربر");
                RefreshPage();
            }
            else
            {
                _messageBoxStore.Show("هنگام حذف کاربر مشکلی بوجود امده است", "حذف کاربر");
            }
        }

        /// <summary>
        /// clear inputs data
        /// </summary>
        private void ClearInputs()
        {
            FirstName = "";
            LastName = "";
            Tier = 0;
        }

        /// <summary>
        /// try to add new client to database
        /// </summary>
        private async Task AddNewClient()
        {
            Client newClient = new Client()
            {
                FirstName = FirstName,
                LastName = LastName,
                Tier = Tier
            };
            int result = await _db.ClientsRepository.AddNewClientToDb(newClient);
            if (result > 0)
            {
                _messageBoxStore.Show("کاربر با موفقیت افزوده شد", "افزودن کاربر");
                RefreshPage();
            }
            else
            {
                _messageBoxStore.Show("هنگام افزودن اطلاعات کاربر مشکلی بوجود امده است", "افزودن کاربر");
            }

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

        /// <summary>
        /// remove inputed data 
        /// </summary>
        /// <returns></returns>
        private async Task RefreshPage()
        {
            var clients = await _db.ClientsRepository.GetAllClients();
            Clients.Clear();
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
            ClearInputs();
        }

        #endregion

    }
}
