using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class EditeReservBookCommand : CommandBase, IEditeReservBookCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private ILoanRepository _loanRepository;
        private IMessageBoxStore _messageBoxStore;
        private ILoadBooksCommand _loadBooksCommand;
        private IClientsRepository _clientsRepository;
        private ICloseModalCommand _closeModalCommand;
        private ILoadClientsCommand _loadClientsCommand;
        private IReservedBooksStore _reservedBooksStore;
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksRepository _reservedBooksRepository;
        private ISearchClientNameCommand _searchClientNameCommand;
        private ILoadReservedBooksCommand _loadReservedBooksCommand;
        private IOrderBooksByStateCommand _orderBooksByStateCommand;
        private IOrderBooksBySubjectCommand _orderBooksBySubjectCommand;
        private ISaveReservationDataCommand _saveReservationDataCommand;
        private ISearchBookNameInReservedBookCommand _searchBookNameInReservedBookCommand;
        #endregion


        #region Contructor
        /// <summary>
        /// validate selected reserv and fill to the add edite view modal
        /// </summary>
        public EditeReservBookCommand(
            IBooksStore booksStore,
            IClientsStore clientsStore,
            ILoanRepository loanRepository,
            IMessageBoxStore messageBoxStore,
            ILoadBooksCommand loadBooksCommand,
            IClientsRepository clientsRepository,
            ICloseModalCommand closeModalCommand,
            ILoadClientsCommand loadClientsCommand,
            IReservedBooksStore reservedBooksStore,
            IModalNavigationStore modalNavigationStore,
            IReservedBooksRepository reservedBooksRepository,
            ISearchClientNameCommand searchClientNameCommand,
            IOrderBooksByStateCommand orderBooksByStateCommand,
            ILoadReservedBooksCommand loadReservedBooksCommand,
            IOrderBooksBySubjectCommand orderBooksBySubjectCommand,
            ISaveReservationDataCommand saveReservationDataCommand,
            ISearchBookNameInReservedBookCommand searchBookNameInReservedBookCommand
            )
        {
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _loanRepository = loanRepository;
            _messageBoxStore = messageBoxStore;
            _loadBooksCommand = loadBooksCommand;
            _clientsRepository = clientsRepository;
            _closeModalCommand = closeModalCommand;
            _loadClientsCommand = loadClientsCommand;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            _reservedBooksRepository = reservedBooksRepository;
            _searchClientNameCommand = searchClientNameCommand;
            _orderBooksByStateCommand = orderBooksByStateCommand;
            _loadReservedBooksCommand = loadReservedBooksCommand;
            _orderBooksBySubjectCommand = orderBooksBySubjectCommand;
            _saveReservationDataCommand = saveReservationDataCommand;
            _searchBookNameInReservedBookCommand = searchBookNameInReservedBookCommand;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            if (_reservedBooksStore.SelectedReserv.ID == 0)
            {
                _messageBoxStore.Show("لطفا ابتدا نوبتی را برای ویراش انتخاب کنید", "ویرایش رزرو");
            }
            else
            {
                //IAddEditeReserveBookViewModel ViewModel = new AddEditeReserveBookViewModel(
                //    _booksStore,
                //    _clientsStore,
                //    _messageBoxStore,
                //    _loadBooksCommand,
                //    _closeModalCommand,
                //    _reservedBooksStore,
                //    _loadClientsCommand,
                //    _modalNavigationStore,
                //    _searchClientNameCommand,
                //    _orderBooksByStateCommand,
                //    _loadReservedBooksCommand,
                //    _orderBooksBySubjectCommand,
                //    _saveReservationDataCommand,
                //    _searchBookNameInReservedBookCommand
                //    );
                //ViewModel.LoadBooksCommand.Execute(null);
                //ViewModel.LoadClientsCommand.Execute(null);
                //ViewModel.SelectedReservedBook = _reservedBooksStore.SelectedReserv;
                //_modalNavigationStore.CurrentViewModel = ViewModel;
            }
        }
        #endregion
    }
}
