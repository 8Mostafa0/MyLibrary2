using MyLibrary.ViewModel.Factory;
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
        public NavigateReservedBooksCommand(INavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _booksStore = ClassFactory.CreateBooksStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            //_reservedBooksViewModel = ClassFactory.;
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
