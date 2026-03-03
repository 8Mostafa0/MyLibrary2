using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class SearchBookCommand : CommandBase, ISearchBookCommand
    {
        #region Dependencies
        private ILoansStore _loansStore;
        private ILoansViewModel _loansViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// sarch in loans lost by book name (check book exist in the loand database and then get that book)
        /// </summary>
        /// <param name="loansViewModel"></param>
        /// <param name="loansStore"></param>
        public SearchBookCommand()
        {
            _loansStore = ClassFactory.CreateLoansStore();
            _loansViewModel = ClassFactory.CreateLoansViewModel();
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            string BookName = _loansViewModel.BookName;
            await _loansStore.GetAllLoans($"SELECT * FROM Loans WHERE EXISTS (SELECT 1 FROM Books WHERE Books.Name LIKE N'%{BookName}%' AND Books.Id = Loans.BookId )");
        }
        #endregion
    }
}
