using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class NavigateBooksCommand : CommandBase
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
        public NavigateBooksCommand(INavigationStore navigationStore, IBooksStore booksStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository, BooksRepository booksRepository, IMessageBoxStore messageBoxStore)
        {
            _navigationStore = navigationStore;
            _booksStore = booksStore;
            //_booksViewModel = BooksViewModel.LoadViewModel(_booksStore, loanRepository, reservedBooksRepository, booksRepository, messageBoxStore);

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
