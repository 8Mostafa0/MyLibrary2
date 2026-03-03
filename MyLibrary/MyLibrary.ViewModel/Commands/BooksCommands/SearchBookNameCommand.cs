using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class SearchBookNameCommand : CommandBase, ISearchBookNameCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// Search Book Using Name From BookStore.
        /// </summary>
        /// <param name="booksStore"></param>
        public SearchBookNameCommand()
        {
            _booksStore = ClassFactory.CreateBooksStore();
        }
        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            string SearchSql = $"SELECT * FROM Books WHERE Name LIKE N'%{_booksStore.SearchBookName}%'";
            await _booksStore.GetAllBooks(SearchSql);
        }
        #endregion
    }
}
