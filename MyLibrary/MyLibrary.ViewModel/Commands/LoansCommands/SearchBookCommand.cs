using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class SearchBookCommand : CommandBase, ISearchBookCommand
    {
        #region Dependencies
        private ILoansStore _loansStore;
        #endregion


        #region Contructor
        /// <summary>
        /// sarch in loans lost by book name (check book exist in the loand database and then get that book)
        /// </summary>
        /// <param name="loansStore"></param>
        public SearchBookCommand(ILoansStore loansStore)
        {
            _loansStore = loansStore;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            string BookName = _loansStore.BookName;
            await _loansStore.GetAllLoans($"SELECT * FROM Loans WHERE EXISTS (SELECT 1 FROM Books WHERE Books.Name LIKE N'%{BookName}%' AND Books.Id = Loans.BookId )");
        }
        #endregion
    }
}
