using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class AddNewReservBookCommand : CommandBase, IAddNewReservBookCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private IReservedBooksStore _reservedBooksStore;
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksRepository _reservedBooksRepository;
        private ILoanRepository _loanRepository;
        private IClientsRepository _clientsRepository;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// load addedite reserved book view model to modal navigation view
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="clientsRepository"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="reservedBooksRepository"></param>
        public AddNewReservBookCommand(
            IBooksStore booksStore,
            IClientsStore clientsStore,
            ILoanRepository loanRepository,
            IMessageBoxStore messageBoxStore,
            IClientsRepository clientsRepository,
            IReservedBooksStore reservedBooksStore,
            IModalNavigationStore modalNavigationStore,
            IReservedBooksRepository reservedBooksRepository
            )
        {
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _loanRepository = loanRepository;
            _messageBoxStore = messageBoxStore;
            _clientsRepository = clientsRepository;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>

        public override void Execute(object parameter)
        {
            //_modalNavigationStore.CurrentViewModel = AddEditeReserveBookViewModel.LoadViewModel(_modalNavigationStore, _reservedBooksStore, _clientsStore, _booksStore, _loanRepository, _reservedBooksRepository, _clientsRepository, _messageBoxStore, null);
        }
        #endregion
    }
}
