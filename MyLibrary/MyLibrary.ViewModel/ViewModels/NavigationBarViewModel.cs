using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Commands.LoginCommands;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Stores;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase, INavigationBarViewModel
    {
        #region Dependencies
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private ReservedBooksStore _reservedBooksStore;
        private IMessageBoxStore _messageBoxStore;
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private ILoansStore _loansStore;
        #endregion

        #region Commands
        public ICommand NavigateHomeCommand { get; }
        public ICommand DatabaseCommand { get; }

        public ICommand ClientsCreenCommand { get; }

        public ICommand NavigateBooksCommand { get; }

        public ICommand OpenModalCommand { get; }
        public ICommand NavigateLoansCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        public ICommand NavigateReservedBooksCommand { get; }
        public ICommand CloseAppCommand { get; }
        #endregion

        #region Constructr
        public NavigationBarViewModel(
            NavigationStore navigationStore,
            ReservedBooksStore reservedBooksStore,
            IClientsStore clientsStore,
            IBooksStore booksStore,
            ILoansStore loansStore,
            LoanRepository loanRepository,
            SettingsStore settingsStore,
            BooksRepository booksRepository,
            ReservedBooksRepository reservedBooksRepository,
            ClientsRepository clientsRepository,
            IMessageBoxStore messageBoxStore,
            ModalNavigationStore modalNavigationStore
            )
        {
            _navigationStore = navigationStore;
            _reservedBooksStore = reservedBooksStore;
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            _loansStore = loansStore;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;
            NavigateHomeCommand = new NavigateHomeScreenCommand(_navigationStore, _loansStore, _clientsStore, _booksStore);
            NavigateHomeCommand.Execute(null);
            ClientsCreenCommand = new NavigateClientScreenCommand(_navigationStore, _clientsStore, loanRepository, reservedBooksRepository, _messageBoxStore);
            NavigateBooksCommand = new NavigateBooksCommand(_navigationStore, _booksStore, loanRepository, reservedBooksRepository, booksRepository, _messageBoxStore);
            NavigateLoansCommand = new NavigateLoansCommand(_navigationStore, _modalNavigationStore, _loansStore, _clientsStore, _booksStore, loanRepository, settingsStore, booksRepository, reservedBooksRepository, _messageBoxStore);
            NavigateReservedBooksCommand = new NavigateReservedBooksCommand(_navigationStore, _modalNavigationStore, _reservedBooksStore, clientsStore, booksStore, loanRepository, clientsRepository, reservedBooksRepository, _messageBoxStore);
            NavigateToSettingsCommand = new NavigateToSettingsCommand(_navigationStore, _messageBoxStore);
            CloseAppCommand = new CloseAppCommand(_messageBoxStore);
        }
        #endregion
    }
}
