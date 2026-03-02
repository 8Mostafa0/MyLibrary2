using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ShowEditLoanViewModel : CommandBase
    {
        #region Dependencies
        private LoansStore _loansStore;
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private SettingsStore _settingsStore;
        private LoansViewModel _loansViewModel;
        private LoanRepository _loanRepository;
        private BooksRepository _booksRepository;
        private MessageBoxStore _messageBoxStore;
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
        /// <param name="MessageBoxStore"></param>
        /// <param name="reservedBooksRepository"></param>
        public ShowEditLoanViewModel(ModalNavigationStore modalNavigationStore, LoansStore loansStore, IBooksStore booksStore, IClientsStore clientsStore, LoansViewModel loansViewModel, LoanRepository loanRepository, SettingsStore settingsStore, BooksRepository booksRepository, MessageBoxStore messageBoxStore, ReservedBooksRepository reservedBooksRepository)
        {
            _loansStore = loansStore;
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _settingsStore = settingsStore;
            _loanRepository = loanRepository;
            _loansViewModel = loansViewModel;
            _booksRepository = booksRepository;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {

            if (_loansViewModel.SelectedLoan is null || _loansViewModel.SelectedLoan._loan is null)
            {
                _messageBoxStore.Show("لطفا ابتدا امانتی را انتخاب کنید", "ویرایش نوبت");
            }
            else if (!(_loansViewModel.SelectedLoan.ReturnedDateTime is null) && _loansViewModel.SelectedLoan.ReturnedDateTime != "خیر")
            {
                _messageBoxStore.Show("این امانت تحویل داده شده است", "ویرایش نوبت");
            }
            else
            {
                //_addEditeLoanViewModel = AddEditeLoanViewModel.LoadViewModel(_modalNavigationStore, _booksStore, _clientsStore, _loansStore, _loanRepository, _settingsStore, _booksRepository, _reservedBooksRepository, _messageBoxStore, loan: !(_loansViewModel.SelectedLoan._loan is null) ? _loansViewModel.SelectedLoan.ToLoan() : null);
                _modalNavigationStore.CurrentViewModel = _addEditeLoanViewModel;
            }
        }
        #endregion
    }
}
