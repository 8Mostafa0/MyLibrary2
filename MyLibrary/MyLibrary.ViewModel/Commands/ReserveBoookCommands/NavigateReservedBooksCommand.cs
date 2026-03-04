using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class NavigateReservedBooksCommand : CommandBase, INavigateReservedBooksCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private IMessageBoxStore _messageBoxStore;
        private INavigationStore _navigationStore;
        private IReservedBooksStore _reservedBooksStore;
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksViewModel _reservedBooksViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="navigationStore"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="reservedBooksViewModel"></param>
        public NavigateReservedBooksCommand(
            IBooksStore booksStore,
            IClientsStore clientsStore,
            IMessageBoxStore messageBoxStore,
            INavigationStore navigationStore,
            IReservedBooksStore reservedBooksStore,
            IModalNavigationStore modalNavigationStore,
            IReservedBooksViewModel reservedBooksViewModel
            )
        {
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _messageBoxStore = messageBoxStore;
            _navigationStore = navigationStore;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            _reservedBooksViewModel = reservedBooksViewModel;
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
