using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ShowEditLoanViewModel : CommandBase, IShowEditLoanViewModel
    {
        #region Dependencies
        private ILoansStore _loansStore;
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private ISettingsStore _settingsStore;
        private ILoansViewModel _loansViewModel;
        private ILoanRepository _loanRepository;
        private IBooksRepository _booksRepository;
        private IMessageBoxStore _messageBoxStore;
        private IModalNavigationStore _modalNavigationStore;
        private IAddEditeLoanViewModel _addEditeLoanViewModel;
        private IReservedBooksRepository _reservedBooksRepository;
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
        public ShowEditLoanViewModel(ILoansViewModel loansViewModel)
        {
            _loansViewModel = loansViewModel;
            _loansStore = ClassFactory.CreateLoansStore();
            _booksStore = ClassFactory.CreateBooksStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _settingsStore = ClassFactory.CreateSettingsStore();
            _loanRepository = ClassFactory.CreateLoanRepository();
            _booksRepository = ClassFactory.CreateBooksRepository();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            _reservedBooksRepository = ClassFactory.CreateReservedBooksRepository();
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
