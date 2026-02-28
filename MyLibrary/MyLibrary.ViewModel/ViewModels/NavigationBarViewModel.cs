using MyLibrary.ViewModel.ViewModels.Interfaces;

namespace MyLibrary.ViewModel.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase, INavigationBarViewModel
    {
        #region Dependencies
        private readonly INavigationStore _navigationStore;
        private readonly IModalNavigationStore _modalNavigationStore;
        private IReservedBooksStore _reservedBooksStore;
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private ILoansStore _loansStore;
        #endregion

        #region Commands
        public INavigateHomeScreenCommand NavigateHomeCommand { get; }
        public ICommand DatabaseCommand { get; }

        public INavigateClientScreenCommand ClientsCreenCommand { get; }

        public INavigateBooksCommand NavigateBooksCommand { get; }

        public IOpenModalCommand OpenModalCommand { get; }
        public INavigateLoansCommand NavigateLoansCommand { get; }
        public INavigateToSettingsCommand NavigateToSettingsCommand { get; }
        public INavigateReservedBooksCommand NavigateReservedBooksCommand { get; }
        public ICloseAppCommand CloseAppCommand { get; }
        #endregion

        #region Constructr
        public NavigationBarViewModel(
            INavigationStore navigationStore,
            IReservedBooksStore reservedBooksStore,
            IClientsStore clientsStore,
            IBooksStore booksStore,
            ILoansStore loansStore,
            ILoanRepository loanRepository,
            ISettingsStore settingsStore,
            IBooksRepository booksRepository,
            IReservedBooksRepository reservedBooksRepository,
            IClientsRepository clientsRepository
            )
        {
            _navigationStore = navigationStore;
            _reservedBooksStore = reservedBooksStore;
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            _loansStore = loansStore;
            _modalNavigationStore = new ModalNavigationStore();
            NavigateHomeCommand = new NavigateHomeScreenCommand(_navigationStore, _loansStore, _clientsStore, _booksStore);
            NavigateHomeCommand.Execute(null);
            ClientsCreenCommand = new NavigateClientScreenCommand(_navigationStore, _clientsStore, loanRepository, reservedBooksRepository);
            NavigateBooksCommand = new NavigateBooksCommand(_navigationStore, _booksStore, loanRepository, reservedBooksRepository, booksRepository);
            NavigateLoansCommand = new NavigateLoansCommand(_navigationStore, _modalNavigationStore, _loansStore, _clientsStore, _booksStore, loanRepository, settingsStore, booksRepository, reservedBooksRepository);
            NavigateToSettingsCommand = new NavigateToSettingsCommand(_navigationStore);
            NavigateReservedBooksCommand = new NavigateReservedBooksCommand(_navigationStore, _modalNavigationStore, _reservedBooksStore, clientsStore, booksStore, loanRepository, clientsRepository, reservedBooksRepository);
            CloseAppCommand = new CloseAppCommand();
        }
        #endregion
    }
}
