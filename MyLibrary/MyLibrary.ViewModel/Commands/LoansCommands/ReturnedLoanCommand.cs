using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ReturnedLoanCommand : CommandBase, IReturnedLoanCommand
    {
        #region Dependencies
        private ILoansStore _loansStore;
        private ILoansViewModel _loanViewModel;
        private IMessageBoxStore _messageBoxStore;
        private IReturnedLoanCommand _returnedLoanCommand;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// check and validate selected loan and
        /// set ReturnedDate to now 
        /// </summary>
        /// <param name="loansViewModel"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="loansStore"></param>
        public ReturnedLoanCommand(
            ILoansStore loansStore,
            ILoansViewModel loansViewModel,
            IMessageBoxStore messageBoxStore,
            IReturnedLoanCommand returnedLoanCommand)
        {
            _loansStore = loansStore;
            _loanViewModel = loansViewModel;
            _messageBoxStore = messageBoxStore;
            _returnedLoanCommand = returnedLoanCommand;
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
            _messageBoxStore.Show("کاربر کتاب را بازگرداند؟", "برگشت کتاب", "بله", "خیر", _returnedLoanCommand);
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
