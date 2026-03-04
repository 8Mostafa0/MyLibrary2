using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ShowLoanModalCommand : CommandBase, IShowLoanModalCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private ILoansStore _loansStore;
        private IClientsStore _clientsStore;
        private ISettingsStore _settingsStore;
        private ILoanRepository _loanRepository;
        private IBooksRepository _booksRepository;
        private IMessageBoxStore _messageBoxStore;
        private ILoadBooksCommand _loadBooksCommand;
        private ICloseModalCommand _closeModalCommand;
        private ILoadClientsCommand _loadClientsCommand;
        private ISaveLoanDataCommand _saveLoanDataCommand;
        private IModalNavigationStore _modalNavigationStore;
        private IAddEditeLoanViewModel _addEditeLoanViewModel;
        private ISearchBookNameCommand _searchBookNameCommand;
        private IReservedBooksRepository _reservedBooksRepository;
        private ISearchClientNameCommand _searchClientNameCommand;
        private IOrderBooksByStateCommand _orderBooksByStateCommand;
        private IOrderBooksBySubjectCommand _orderBooksBySubjectCommand;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// show loan modal by set modal view to the loan modal
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="addEditeLoanViewModel"></param>
        public ShowLoanModalCommand(
            IBooksStore booksStore,
            ILoansStore loansStore,
            IClientsStore clientsStore,
            ISettingsStore settingsStore,
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
            _booksStore = booksStore;
            _loansStore = loansStore;
            _clientsStore = clientsStore;
            _settingsStore = settingsStore;
            _loanRepository = loanRepository;
            _booksRepository = booksRepository;
            _messageBoxStore = messageBoxStore;
            _loadBooksCommand = loadBooksCommand;
            _closeModalCommand = closeModalCommand;
            _loadClientsCommand = loadClientsCommand;
            _saveLoanDataCommand = saveLoanDataCommand;
            _searchBookNameCommand = searchBookNameCommand;
            _reservedBooksRepository = reservedBooksRepository;
            _searchClientNameCommand = searchClientNameCommand;
            _orderBooksByStateCommand = orderBooksByStateCommand;
            _orderBooksBySubjectCommand = orderBooksBySubjectCommand;
            _modalNavigationStore = modalNavigationStore;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _loansStore.SelectedLoan = new LoanViewModel(new Loan() { Id = 0 }, _clientsStore, _booksStore);
            _addEditeLoanViewModel = new AddEditeLoanViewModel(
                _booksStore,
                _loansStore,
                _clientsStore,
                _settingsStore,
                _loanRepository,
                _booksRepository,
                _messageBoxStore,
                _loadBooksCommand,
                _closeModalCommand,
                _loadClientsCommand,
                _saveLoanDataCommand,
                _modalNavigationStore,
                _searchBookNameCommand,
                _reservedBooksRepository,
                _searchClientNameCommand,
                _orderBooksByStateCommand,
                _orderBooksBySubjectCommand
                );
            _modalNavigationStore.CurrentViewModel = _addEditeLoanViewModel;
        }
        #endregion
    }
}
