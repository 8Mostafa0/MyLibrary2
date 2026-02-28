using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class NavigateBooksCommand : CommandBase
    {
        #region Dipendencies
        private BooksStore _booksStore;
        private BooksViewModel _booksViewModel;
        private NavigationStore _navigationStore;
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
        public NavigateBooksCommand(NavigationStore navigationStore, BooksStore booksStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository, BooksRepository booksRepository)
        {
            _navigationStore = navigationStore;
            _booksStore = booksStore;
            _booksViewModel = BooksViewModel.LoadViewModel(_booksStore, loanRepository, reservedBooksRepository, booksRepository);

        }

        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object? parameter)
        {
            _navigationStore.ContentScreen = _booksViewModel;
        }
        #endregion
    }
}
