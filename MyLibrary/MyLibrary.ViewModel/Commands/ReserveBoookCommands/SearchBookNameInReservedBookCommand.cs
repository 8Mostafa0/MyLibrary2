using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class SearchBookNameInReservedBookCommand : CommandBase, ISearchBookNameInReservedBookCommand
    {
        #region Dependencies
        private IReservedBooksStore _reservedBooksStore;
        private IReservedBooksViewModel _reservedBooksViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// search book name in reserved books list
        /// </summary>
        public SearchBookNameInReservedBookCommand()
        {
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
            _reservedBooksViewModel = ClassFactory.CreateReservedBooksViewModel();
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
