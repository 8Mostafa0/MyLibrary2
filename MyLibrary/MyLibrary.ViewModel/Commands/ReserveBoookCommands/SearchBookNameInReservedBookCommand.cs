using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class SearchBookNameInReservedBookCommand : CommandBase, ISearchBookNameInReservedBookCommand
    {
        #region Dependencies
        private IReservedBooksStore _reservedBooksStore;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// search book name in reserved books list
        /// </summary>
        /// <param name="messageBoxStore"></param>
        /// <param name="reservedBooksStore"></param>
        public SearchBookNameInReservedBookCommand(
            IMessageBoxStore messageBoxStore,
            IReservedBooksStore reservedBooksStore)
        {
            _messageBoxStore = messageBoxStore;
            _reservedBooksStore = reservedBooksStore;
        }
        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            if (_reservedBooksStore.SelectedBook is null || _reservedBooksStore.SelectedBook.Name == "")
            {
                _messageBoxStore.Show("لطفا نام کتاب را وراد کنید", "جستجوی کتاب");
                return;
            }
            string SearchSql = $"SELECT * FROM ReservedBooks WHERE EXISTS (SELECT 1 FROM Books WHERE Books.Name LIKE N'%{_reservedBooksStore.SelectedBook.Name}%' AND Books.Id = ReservedBooks.BookId )";
            await _reservedBooksStore.GetReservedBooksAsync(SearchSql);
        }
        #endregion
    }
}
