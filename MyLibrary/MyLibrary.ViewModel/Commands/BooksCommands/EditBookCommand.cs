using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class EditBookCommand : CommandBase, IEditBookCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IBooksViewModel _booksViewModel;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="booksRepository"></param>
        /// <param name="messageBoxStore"></param>
        public EditBookCommand(
            IBooksStore booksStore,
            IMessageBoxStore messageBoxStore)
        {
            _booksStore = booksStore;
            _messageBoxStore = messageBoxStore;
        }
        #endregion


        #region Execution
        /// <summary>
        ///  Checks To Validate Book Data First then Edite Selected Using BookStore Class.
        /// </summary>
        /// <param name="parameter">No Perameer Needed This Method Gets Its Data From BooksViewModel 
        /// Data : Name,Publisher,Subject,PublicationDate</param>
        public override async void Execute(object parameter)
        {
            Book SelectedBook = _booksStore.SelectedBook;
            if (SelectedBook == null)
            {
                _messageBoxStore.Show("لطفا کتابی را برای ویرایش انتخاب کنید", "ویرایش کتاب");
            }

            if (_booksStore.SelectedBook.Name is null || _booksStore.SelectedBook.Name == "")
            {
                _messageBoxStore.Show("لطفا نام کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksStore.SelectedBook.Publisher is null || _booksStore.SelectedBook.Publisher == "")
            {
                _messageBoxStore.Show("لطفا منتشرکننده کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksStore.SelectedBook.Subject is null || _booksStore.SelectedBook.Subject == "")
            {
                _messageBoxStore.Show("لطفا نوع کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksStore.SelectedBook.PublicationDate is null || _booksStore.SelectedBook.PublicationDate == "")
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            int PublicationYear = 0;
            if (!int.TryParse(_booksStore.SelectedBook.PublicationDate, out PublicationYear) || _booksStore.SelectedBook.PublicationDate.Length != 4)
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را عدد 4 رقمی وارد کنید", "افزودن کتاب");
                return;
            }



            SelectedBook.Name = _booksStore.SelectedBook.Name;
            SelectedBook.Publisher = _booksStore.SelectedBook.Publisher;
            SelectedBook.Subject = _booksStore.SelectedBook.Subject;
            SelectedBook.PublicationDate = _booksStore.SelectedBook.PublicationDate;
            SelectedBook.Tier = _booksStore.SelectedBook.Tier;
            await _booksStore.EditBook(SelectedBook);
        }
        #endregion
    }
}
