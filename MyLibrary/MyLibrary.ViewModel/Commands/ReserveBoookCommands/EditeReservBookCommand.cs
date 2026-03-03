using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class EditeReservBookCommand : CommandBase, IEditeReservBookCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private ILoanRepository _loansRepository;
        private IClientsRepository _clientRepository;
        private IReservedBooksStore _reservedBooksStore;
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksViewModel _reservedBooksViewModel;
        private IReservedBooksRepository _reservedBooksRepository;
        private IMessageBoxStore _messageBoxStore;
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
            IClientsRepository clientsRepository,
            IReservedBooksStore reservedBooksStore,
            IModalNavigationStore modalNavigationStore,
            IReservedBooksRepository reservedBooksRepository,
            IReservedBooksViewModel reservedBooksViewModel
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
