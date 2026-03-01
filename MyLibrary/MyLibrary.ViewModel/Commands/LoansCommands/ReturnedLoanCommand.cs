using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ReturnedLoanCommand : CommandBase
    {
        #region Dependencies
        private LoansStore _loansStore;
        private LoansViewModel _loanViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// check and validate selected loan and
        /// set ReturnedDate to now 
        /// </summary>
        /// <param name="loansViewModel"></param>
        /// <param name="loansStore"></param>
        public ReturnedLoanCommand(LoansViewModel loansViewModel, LoansStore loansStore)
        {
            _loanViewModel = loansViewModel;
            _loansStore = loansStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object? parameter)
        {
            LoanViewModel loan = _loanViewModel.SelectedLoan;
            if (loan != null)
            {
                bool IsReturnedLoan = DateTime.TryParse(loan.ReturnedDateTime, out DateTime _);
                if (IsReturnedLoan)
                {
                    MessageBox.Show("این امانت بارگشت داده  شده است", "برگشت کتاب");
                    return;
                }
                var AskMessage = MessageBox.Show("کاربر کتاب را بازگرداند؟", "برگشت کتاب", MessageBoxButton.YesNo);
                if (AskMessage == MessageBoxResult.Yes)
                {
                    loan.ReturnedDateTime = DateTime.Now.ToString();
                    await _loansStore.LoanReturned(loan.ToLoan());
                }
            }
            else
            {
                MessageBox.Show("لطفا ابتدا ایتمی را انتخاب کنید", "برگشت کتاب");
            }
        }
        #endregion
    }
}
