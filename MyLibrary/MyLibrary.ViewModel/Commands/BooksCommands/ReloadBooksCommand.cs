using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class ReloadBooksCommand : CommandBase, IReloadBooksCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        public ReloadBooksCommand()
        {
            _booksStore = ClassFactory.CreateBooksStore();
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
