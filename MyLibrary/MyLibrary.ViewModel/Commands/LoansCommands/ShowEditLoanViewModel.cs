using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ShowEditLoanViewModel : CommandBase, IShowEditLoanViewModel
    {
        #region Dependencies
        private ILoansStore _loanStore;
        private IMessageBoxStore _messageBoxStore;
        private IModalNavigationStore _modalNavigationStore;
        private IAddEditeLoanViewModel _addEditeLoanViewModel;
        private IReservedBooksRepository _reservedBooksRepository;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// validate loan be not returned and fill loan data to addedit view model
        /// </summary>
        /// <param name="loansStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="addEditeLoanViewModel"></param>
        /// <param name="reservedBooksRepository"></param>
        public ShowEditLoanViewModel(
            ILoansStore loansStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore,
            IAddEditeLoanViewModel addEditeLoanViewModel,
            IReservedBooksRepository reservedBooksRepository)
        {
            _loanStore = loansStore;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;
            _addEditeLoanViewModel = addEditeLoanViewModel;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {

            if (_loanStore.SelectedLoan is null || _loanStore.SelectedLoan._loan is null)
            {
                _messageBoxStore.Show("لطفا ابتدا امانتی را انتخاب کنید", "ویرایش نوبت");
            }
            else if (!(_loanStore.SelectedLoan.ReturnedDateTime is null) && _loanStore.SelectedLoan.ReturnedDateTime != "خیر")
            {
                _messageBoxStore.Show("این امانت تحویل داده شده است", "ویرایش نوبت");
            }
            else
            {
                _addEditeLoanViewModel.LoadClientsCommand.Execute(null);
                _addEditeLoanViewModel.LoadBooksCommand.Execute(null);
                _modalNavigationStore.CurrentViewModel = _addEditeLoanViewModel;
            }
        }
        #endregion
    }
}
