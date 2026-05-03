using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ShowEditLoanViewModel : CommandBase, IShowEditLoanViewModel
    {
        #region Dependencies
        private IApplicationStore _applicationStore;
        private IMyLibraryDbContext _db;
        private IMessageBoxStore _messageBoxStore;
        private ILoadBooksCommand _loadBooksCommand;
        private ICloseModalCommand _closeModalCommand;
        private ILoadClientsCommand _loadClientsCommand;
        private ISaveLoanDataCommand _saveLoanDataCommand;
        private IModalNavigationStore _modalNavigationStore;
        private IAddEditeLoanViewModel _addEditeLoanViewModel;
        private ISearchBookNameCommand _searchBookNameCommand;
        private ISearchClientNameCommand _searchClientNameCommand;
        private IOrderBooksByStateCommand _orderBooksByStateCommand;
        private IOrderBooksBySubjectCommand _orderBooksBySubjectCommand;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// validate loan be not returned and fill loan data to addedit view model
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="settingsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="booksRepository"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="loadBooksCommand"></param>
        /// <param name="closeModalCommand"></param>
        /// <param name="loadClientsCommand"></param>
        /// <param name="saveLoanDataCommand"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="searchBookNameCommand"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="searchClientNameCommand"></param>
        /// <param name="orderBooksByStateCommand"></param>
        /// <param name="orderBooksBySubjectCommand"></param>
        public ShowEditLoanViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            ILoanRepository loanRepository,
            IBooksRepository booksRepository,
            IMessageBoxStore messageBoxStore,
            ILoadBooksCommand loadBooksCommand,
            ICloseModalCommand closeModalCommand,
            ILoadClientsCommand loadClientsCommand,
            ISaveLoanDataCommand saveLoanDataCommand,
            IModalNavigationStore modalNavigationStore,
            ISearchBookNameCommand searchBookNameCommand,
            IReservedBooksRepository reservedBooksRepository,
            ISearchClientNameCommand searchClientNameCommand,
            IOrderBooksByStateCommand orderBooksByStateCommand,
            IOrderBooksBySubjectCommand orderBooksBySubjectCommand
            )
        {
            _db = db;
            _applicationStore = applicationStore;
            _messageBoxStore = messageBoxStore;
            _loadBooksCommand = loadBooksCommand;
            _closeModalCommand = closeModalCommand;
            _loadClientsCommand = loadClientsCommand;
            _saveLoanDataCommand = saveLoanDataCommand;
            _modalNavigationStore = modalNavigationStore;
            _searchBookNameCommand = searchBookNameCommand;
            _searchClientNameCommand = searchClientNameCommand;
            _orderBooksBySubjectCommand = orderBooksBySubjectCommand;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {

            _addEditeLoanViewModel = await AddEditeLoanViewModel.InitlizeViewModel(
                _db,
                _applicationStore,
                _messageBoxStore,
                _modalNavigationStore
                );
            _modalNavigationStore.CurrentViewModel = _addEditeLoanViewModel;
        }
    }
    #endregion
}

