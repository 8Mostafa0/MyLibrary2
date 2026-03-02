using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class NavigateReservedBooksCommand : CommandBase
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private MessageBoxStore _messageBoxStore;
        private NavigationStore _navigationStore;
        private ReservedBooksStore _reservedBooksStore;
        private ModalNavigationStore _modalNavigationStore;
        private ReservedBooksViewModel _reservedBooksViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loansRepository"></param>
        /// <param name="clientsRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public NavigateReservedBooksCommand(
            NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            ReservedBooksStore reservedBooksStore,
            IClientsStore clientsStore,
            IBooksStore booksStore,
            LoanRepository loansRepository,
            ClientsRepository clientsRepository,
            ReservedBooksRepository reservedBooksRepository,
            MessageBoxStore messageBoxStore
            )
        {
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _messageBoxStore = messageBoxStore;
            _navigationStore = navigationStore;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            //_reservedBooksViewModel = ReservedBooksViewModel.LoadViewModel(_reservedBooksStore, _modalNavigationStore, _clientsStore, _booksStore, loansRepository, clientsRepository, reservedBooksRepository, _messageBoxStore);
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _navigationStore.ContentScreen = _reservedBooksViewModel;
        }
        #endregion
    }
}
