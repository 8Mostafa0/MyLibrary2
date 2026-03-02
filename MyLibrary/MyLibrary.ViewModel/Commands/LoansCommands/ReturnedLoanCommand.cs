using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ReturnedLoanCommand : CommandBase
    {
        #region Dependencies
        private ILoansStore _loansStore;
        private LoansViewModel _loanViewModel;
        private MessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// check and validate selected loan and
        /// set ReturnedDate to now 
        /// </summary>
        /// <param name="loansViewModel"></param>
        /// <param name="loansStore"></param>
        public ReturnedLoanCommand(LoansViewModel loansViewModel, ILoansStore loansStore, MessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
            _loanViewModel = loansViewModel;
            _loansStore = loansStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            LoanViewModel loan = _loanViewModel.SelectedLoan;
            if (loan._loan == null || loan == null)
            {
                _messageBoxStore.Show("لطفا ابتدا ایتمی را انتخاب کنید", "برگشت کتاب");
                return;
            }
            bool IsReturnedLoan = DateTime.TryParse(loan.ReturnedDateTime, out DateTime _);
            if (IsReturnedLoan)
            {
                _messageBoxStore.Show("این امانت بارگشت داده  شده است", "برگشت کتاب");
                return;
            }
            _messageBoxStore.Show("کاربر کتاب را بازگرداند؟", "برگشت کتاب", "بله", "خیر", new ReturnedLoanCommand(_loanViewModel, _loansStore, _messageBoxStore));
            if (_messageBoxStore.MessageBoxResult)
            {
                _messageBoxStore.CloseMessageBox();
                loan.ReturnedDateTime = DateTime.Now.ToString();
                await _loansStore.LoanReturned(loan.ToLoan());
            }
        }
        #endregion
    }
}
