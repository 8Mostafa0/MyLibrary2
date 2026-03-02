using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class SearchBookNameInReservedBookCommand : CommandBase, ISearchBookNameInReservedBookCommand
    {
        #region Dependencies
        private IReservedBooksStore _reservedBooksStore;
        private ReservedBooksViewModel _reservedBooksViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// search book name in reserved books list
        /// </summary>
        /// <param name="reservedBooksViewModel"></param>
        /// <param name="reservedBooksStore"></param>
        public SearchBookNameInReservedBookCommand(ReservedBooksViewModel reservedBooksViewModel, IReservedBooksStore reservedBooksStore)
        {
            _reservedBooksStore = reservedBooksStore;
            _reservedBooksViewModel = reservedBooksViewModel;
        }
        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            if (!(_reservedBooksViewModel.BookName == "") && !(_reservedBooksViewModel.BookName is null))
            {
                _reservedBooksStore.Clear();
                string SearchSql = $"SELECT * FROM ReservedBooks WHERE EXISTS (SELECT 1 FROM Books WHERE Books.Name LIKE N'%{_reservedBooksViewModel.BookName}%' AND Books.Id = ReservedBooks.BookId )";
                await _reservedBooksStore.GetReservedBooksAsync(SearchSql);
            }
        }
        #endregion
    }
}
