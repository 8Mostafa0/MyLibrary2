using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class AddNewReservBookCommand : CommandBase
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private ReservedBooksStore _reservedBooksStore;
        private ModalNavigationStore _modalNavigationStore;
        private ReservedBooksRepository _reservedBooksRepository;
        private LoanRepository _loanRepository;
        private ClientsRepository _clientsRepository;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load addedite reserved book view model to modal navigation view
        /// </summary>
        /// <param name="reservedBooksViewModel"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="clientsRepository"></param>
        public AddNewReservBookCommand(
            ModalNavigationStore modalNavigationStore,
            ReservedBooksStore reservedBooksStore,
            IClientsStore clientsStore,
            IBooksStore booksStore,
            LoanRepository loanRepository,
            ReservedBooksRepository reservedBooksRepository,
            ClientsRepository clientsRepository,
            IMessageBoxStore messageBoxStore
            )
        {
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            _reservedBooksRepository = reservedBooksRepository;
            _loanRepository = loanRepository;
            _clientsRepository = clientsRepository;
            _messageBoxStore = messageBoxStore;
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
