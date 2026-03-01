using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class LoadBooksCommand : CommandBase
    {
        #region Dependencies
        private readonly BooksStore _booksStore;
        #endregion

        #region Contructor
        /// <summary>
        /// Load All The Books From Database To Store
        /// </summary>
        /// <param name="booksStore"></param>
        public LoadBooksCommand(BooksStore booksStore)
        {
            _booksStore = booksStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            await _booksStore.GetAllBooks();
        }
        #endregion
    }
}
