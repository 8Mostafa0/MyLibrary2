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
        public override async void Execute(object parameter)
        {

            if (_loansStore.SelectedLoan is null || _loansStore.SelectedLoan._loan is null)
            {
                _messageBoxStore.Show("لطفا ابتدا امانتی را انتخاب کنید", "ویرایش نوبت");
            }
            else if (!(_loansStore.SelectedLoan.ReturnedDateTime is null) && _loansStore.SelectedLoan.ReturnedDateTime != "خیر")
            {
                _messageBoxStore.Show("این امانت تحویل داده شده است", "ویرایش نوبت");
            }
            else
            {
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
                    )
                {
                    SelectedLoan = _loansStore.SelectedLoan._loan
                };
                _modalNavigationStore.CurrentViewModel = _addEditeLoanViewModel;
            }
        }
        #endregion
    }
}
