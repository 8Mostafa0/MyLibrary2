namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ShowEditLoanViewModel : CommandBase
    {
        #region Dependencies
        private LoansStore _loansStore;
        private BooksStore _booksStore;
        private ClientsStore _clientsStore;
        private SettingsStore _settingsStore;
        private LoansViewModel _loansViewModel;
        private LoanRepository _loanRepository;
        private BooksRepository _booksRepository;
        private ModalNavigationStore _modalNavigationStore;
        private AddEditeLoanViewModel _addEditeLoanViewModel;
        private ReservedBooksRepository _reservedBooksRepository;
        #endregion


        #region Contructor
        /// <summary>
        /// validate loan be not returned and fill loan data to addedit view model
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loansViewModel"></param>
        /// <param name="loanRepository"></param>
        /// <param name="settingsStore"></param>
        /// <param name="booksRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public ShowEditLoanViewModel(ModalNavigationStore modalNavigationStore, LoansStore loansStore, BooksStore booksStore, ClientsStore clientsStore, LoansViewModel loansViewModel, LoanRepository loanRepository, SettingsStore settingsStore, BooksRepository booksRepository, ReservedBooksRepository reservedBooksRepository)
        {
            _loansStore = loansStore;
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _settingsStore = settingsStore;
            _loanRepository = loanRepository;
            _loansViewModel = loansViewModel;
            _booksRepository = booksRepository;
            _modalNavigationStore = modalNavigationStore;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object? parameter)
        {
            if (_loansViewModel.SelectedLoan is null)
            {
                MessageBox.Show("لطفا ابتدا امانتی را انتخاب کنید", "ویرایش نوبت");
            }
            else if (_loansViewModel.SelectedLoan.ReturnedDate is not null && _loansViewModel.SelectedLoan.ReturnedDate != "خیر")
            {
                MessageBox.Show("این امانت تحویل داده شده است", "ویرایش نوبت");
            }
            else
            {

                _addEditeLoanViewModel = AddEditeLoanViewModel.LoadViewModel(_modalNavigationStore, _booksStore, _clientsStore, _loansStore, _loanRepository, _settingsStore, _booksRepository, _reservedBooksRepository, loan: _loansViewModel.SelectedLoan.ToLoan());
                _modalNavigationStore.CurrentViewModel = _addEditeLoanViewModel;
            }
        }
        #endregion
    }
}
