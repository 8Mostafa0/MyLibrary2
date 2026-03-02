using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class EditeReservBookCommand : CommandBase
    {
        #region Dependencies
        private BooksStore _booksStore;
        private ClientsStore _clientsStore;
        private LoanRepository _loansRepository;
        private ClientsRepository _clientRepository;
        private ReservedBooksStore _reservedBooksStore;
        private ModalNavigationStore _modalNavigationStore;
        private ReservedBooksViewModel _reservedBooksViewModel;
        private ReservedBooksRepository _reservedBooksRepository;
        private MessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// validate selected reserv and fill to the add edite view modal
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="clientsRepository"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="reservedBooksViewModel"></param>
        /// <param name="reservedBooksRepository"></param>
        public EditeReservBookCommand(
            BooksStore booksStore,
            ClientsStore clientsStore,
            LoanRepository loanRepository,
            ClientsRepository clientsRepository,
            ReservedBooksStore reservedBooksStore,
            ModalNavigationStore modalNavigationStore,
            ReservedBooksViewModel reservedBooksViewModel,
            ReservedBooksRepository reservedBooksRepository,
            MessageBoxStore messageBoxStore
            )
        {
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _loansRepository = loanRepository;
            _messageBoxStore = messageBoxStore;
            _clientRepository = clientsRepository;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            _reservedBooksViewModel = reservedBooksViewModel;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            if (_reservedBooksViewModel.SelectedReservedBook is null)
            {
                _messageBoxStore.Show("لطفا ابتدا نوبتی را برای ویراش انتخاب کنید", "ویرایش رزرو");
            }
            else
            {
                //_modalNavigationStore.CurrentViewModel = AddEditeReserveBookViewModel.LoadViewModel(
                //    _modalNavigationStore,
                //    _reservedBooksStore,
                //    _clientsStore,
                //    _booksStore,
                //    _loansRepository,
                //    _reservedBooksRepository,
                //    _clientRepository,
                //    _messageBoxStore,
                //    _reservedBooksViewModel.SelectedReservedBook?.ToReservedBook()
                //    );
            }
        }
        #endregion
    }
}
