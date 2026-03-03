using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class LoadBooksCommand : CommandBase, ILoadBooksCommand
    {
        #region Dependencies
        private readonly IBooksStore _booksStore;
        #endregion

        #region Contructor
        /// <summary>
        /// Load All The Books From Database To Store
        /// </summary>
        /// <param name="booksStore"></param>
        public LoadBooksCommand(IBooksStore booksStore)
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
