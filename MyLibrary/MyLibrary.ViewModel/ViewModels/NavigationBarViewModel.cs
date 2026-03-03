using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Commands.LoginCommands;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Factory;
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
        public NavigationBarViewModel(INavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _booksStore = ClassFactory.CreateBooksStore();
            _loansStore = ClassFactory.CreateLoansStore();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            NavigateHomeCommand = ClassFactory.CreateNavigateHomeScreenCommand(navigationStore);
            NavigateHomeCommand.Execute(null);
            ClientsCreenCommand = ClassFactory.CreateNavigateClientScreenCommand(navigationStore);
            NavigateBooksCommand = ClassFactory.CreateNavigateBooksScreenCommand(navigationStore);
            NavigateLoansCommand = ClassFactory.CreateNavigateLoansCommand(navigationStore);
            NavigateReservedBooksCommand = ClassFactory.CreateNavigateReservedBooksCommand(navigationStore);
            NavigateToSettingsCommand = ClassFactory.CreateNavigateToSettingsCommand(navigationStore);
            CloseAppCommand = ClassFactory.CreateCloseAppCommand();
        }
        #endregion
    }
}
