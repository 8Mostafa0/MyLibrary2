using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class OrderBooksByStateCommand : CommandBase, IOrderBooksByStateCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        #endregion


        #region Contructor
        /// <summary>
        ///  Order Book By SortIndex
        ///  1 => Not Returned Loans
        ///  2 => Delaiyed Loans
        /// </summary>
        /// <param name="booksStore"></param>
        public OrderBooksByStateCommand(IBooksStore booksStore)
        {
            _booksStore = booksStore;
        }
        #endregion


        #region Execution

        /// <summary>
        /// Execute Sort Order And Fetch Result From Database
        /// </summary>
        /// <param name="parameter"></param>
        public override async void Execute(object parameter)
        {
            _booksStore.clear();
            string CustomSql = "";

            if (_booksStore.SortIndex == 1)
            {
                CustomSql += "SELECT * FROM Books b WHERE EXISTS ( SELECT 1 FROM Loans l WHERE l.BookId = b.Id AND l.ReturnedDate IS NULL)";
            }
            else if (_booksStore.SortIndex == 2)
            {
                CustomSql = "SELECT * FROM Books WHERE Id IN ( SELECT DISTINCT BookId FROM Loans WHERE ReturnedDate IS NULL AND ReturnDate < GETDATE())";
            }
            await _booksStore.GetAllBooks(CustomSql);

        }
        #endregion
    }
}
