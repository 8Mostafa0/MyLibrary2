using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class SortLoansListCommand : CommandBase, ISortLoansListCommand
    {
        #region Dependencies
        private ILoansStore _loansStore;
        private ILoansViewModel _loansViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// sort loans by Sortindex
        /// 0 to not returned loans
        /// 1 to dilayed loan
        /// 3 to returned loan
        /// </summary>
        /// <param name="loansViewModel"></param>
        /// <param name="loansStore"></param>
        public SortLoansListCommand(ILoansViewModel loansViewModel)
        {
            _loansViewModel = loansViewModel;
            _loansStore = ClassFactory.CreateLoansStore();
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            switch (_loansViewModel.SortIndex)
            {
                case 0: { await _loansStore.GetAllLoans("SELECT * FROM Loans WHERE ReturnedDate IS NULL"); break; }
                case 1: { await _loansStore.GetAllLoans("SELECT * FROM Loans WHERE ReturnDate < GETDATE() AND  ReturnedDate IS NULL"); break; }
                case 2: { await _loansStore.GetAllLoans("SELECT * FROM Loans WHERE ReturnedDate IS NOT NULL"); break; }
            }
        }
        #endregion
    }
}
