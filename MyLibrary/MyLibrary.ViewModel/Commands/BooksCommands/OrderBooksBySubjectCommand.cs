using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class OrderBooksBySubjectCommand : CommandBase, IOrderBooksBySubjectCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// Order Book By Entered Subject
        /// </summary>
        /// <param name="booksStore"></param>
        public OrderBooksBySubjectCommand(IBooksStore booksStore, IMessageBoxStore messageBoxStore)
        {
            _booksStore = booksStore;
            _messageBoxStore = messageBoxStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// Order Book By Entered Subject
        /// </summary>
        /// <param name="parameter"></param>
        public override async void Execute(object parameter)
        {
            if (_booksStore.SearchSubject < 0)
            {
                _messageBoxStore.Show("لطفا ابتدا یک مورد برای ترتیب بندی انتخاب کنید", "ترتیب بندی");
            }
            string SubjectName = "";
            switch (_booksStore.SearchSubject)
            {
                case 0: { SubjectName = "رمان"; break; }
                case 1: { SubjectName = "قصه"; break; }
                case 2: { SubjectName = "آموزشی"; break; }
                case 3: { SubjectName = "معمایی"; break; }
                case 4: { SubjectName = "خودشناسی"; break; }
                case 5: { SubjectName = "شکرگذاری"; break; }
            }
            string SearchSql = $"SELECT * FROM Books WHERE Subject=N'{SubjectName}'";
            await _booksStore.GetAllBooks(SearchSql);

        }
        #endregion
    }
}
