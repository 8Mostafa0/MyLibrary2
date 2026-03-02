using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class NavigateBooksCommand : CommandBase, INavigateBooksCommand
    {
        #region Dipendencies
        private IBooksStore _booksStore;
        private IBooksViewModel _booksViewModel;
        private INavigationStore _navigationStore;
        #endregion


        #region Constructor
        /// <summary>
        /// Navigatie Screen Content To Books View Model
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="booksRepository"></param>
        public NavigateBooksCommand()
        {
            _navigationStore = ClassFactory.CreateNavigationStore();
            _booksStore = ClassFactory.CreateBooksStore();
            _booksViewModel = ClassFactory.CreateBooksViewModel();

        }

        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _navigationStore.ContentScreen = _booksViewModel;
        }
        #endregion
    }
}
