using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class ReloadBooksCommand : CommandBase
    {
        #region Dependencies
        private BooksStore _booksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksStore"></param>
        public ReloadBooksCommand(BooksStore booksStore)
        {
            _booksStore = booksStore;
        }
        #endregion


        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">No Parameter Need</param>
        public override async void Execute(object parameter)
        {
            await _booksStore.GetAllBooks();
        }
        #endregion
    }
}
