using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class OrderBooksByStateCommand : CommandBase, IOrderBooksByStateCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IBooksViewModel _booksViewModel;
        #endregion


        #region Contructor
        /// <summary>
        ///  Order Book By SortIndex
        ///  1 => Not Returned Loans
        ///  2 => Delaiyed Loans
        /// </summary>
        /// <param name="booksViewModel"></param>
        /// <param name="booksStore"></param>
        public OrderBooksByStateCommand(IBooksViewModel booksViewModel, IBooksStore booksStore)
        {
            _booksStore = booksStore;
            _booksViewModel = booksViewModel;
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

            if (_booksViewModel.SortIndex == 1)
            {
                CustomSql += "SELECT * FROM Books b WHERE EXISTS ( SELECT 1 FROM Loans l WHERE l.BookId = b.Id AND l.ReturnedDate IS NULL)";
            }
            else if (_booksViewModel.SortIndex == 2)
            {
                CustomSql = "SELECT * FROM Books WHERE Id IN ( SELECT DISTINCT BookId FROM Loans WHERE ReturnedDate IS NULL AND ReturnDate < GETDATE())";
            }
            await _booksStore.GetAllBooks(CustomSql);

        }
        #endregion
    }
}
