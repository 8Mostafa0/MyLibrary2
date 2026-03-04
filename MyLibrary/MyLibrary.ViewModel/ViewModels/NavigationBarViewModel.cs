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
        private readonly INavigationStore _navigationStore;
        private readonly IModalNavigationStore _modalNavigationStore;
        private IReservedBooksStore _reservedBooksStore;
        private IMessageBoxStore _messageBoxStore;
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private ILoansStore _loansStore;
        #endregion

        #region Commands
        public INavigateHomeScreenCommand NavigateHomeCommand { get; }
        public ICommand DatabaseCommand { get; }

        public INavigateClientScreenCommand ClientsCreenCommand { get; }

        public INavigateBooksCommand NavigateBooksCommand { get; }

        public ICommand OpenModalCommand { get; }
        public INavigateLoansCommand NavigateLoansCommand { get; }
        public INavigateToSettingsCommand NavigateToSettingsCommand { get; }
        public INavigateReservedBooksCommand NavigateReservedBooksCommand { get; }
        public ICloseAppCommand CloseAppCommand { get; }
        #endregion

        #region Constructr
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="navigateHomeScreenCommand"></param>
        /// <param name="navigateClientScreenCommand"></param>
        /// <param name="navigateBooksCommand"></param>
        /// <param name="navigateLoansCommand"></param>
        /// <param name="navigateReservedBooksCommand"></param>
        /// <param name="navigateToSettingsCommand"></param>
        /// <param name="closeAppCommand"></param>
        public NavigationBarViewModel(
            INavigationStore navigationStore,
            IReservedBooksStore reservedBooksStore,
            IClientsStore clientsStore,
            IBooksStore booksStore,
            ILoansStore loansStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore,
            INavigateHomeScreenCommand navigateHomeScreenCommand,
            INavigateClientScreenCommand navigateClientScreenCommand,
            INavigateBooksCommand navigateBooksCommand,
            INavigateLoansCommand navigateLoansCommand,
            INavigateReservedBooksCommand navigateReservedBooksCommand,
            INavigateToSettingsCommand navigateToSettingsCommand,
            ICloseAppCommand closeAppCommand
            )
        {
            _navigationStore = navigationStore;
            _reservedBooksStore = reservedBooksStore;
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            _loansStore = loansStore;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;

            NavigateHomeCommand = navigateHomeScreenCommand;
            ClientsCreenCommand = navigateClientScreenCommand;
            NavigateBooksCommand = navigateBooksCommand;
            NavigateLoansCommand = navigateLoansCommand;
            NavigateReservedBooksCommand = navigateReservedBooksCommand;
            NavigateToSettingsCommand = navigateToSettingsCommand;
            CloseAppCommand = closeAppCommand;

            NavigateHomeCommand.Execute(null);
        }
        #endregion
    }
}
