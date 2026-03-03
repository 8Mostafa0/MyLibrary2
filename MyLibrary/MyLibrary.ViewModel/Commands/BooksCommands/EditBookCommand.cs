using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class EditBookCommand : CommandBase, IEditBookCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IBooksViewModel _booksViewModel;
        private IBooksRepository _booksRepository;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        public EditBookCommand()
        {
            _booksStore = ClassFactory.CreateBooksStore();
            _booksViewModel = ClassFactory.CreateBooksViewModel();
            _booksRepository = ClassFactory.CreateBooksRepository();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
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
            Book SelectedBook = _booksViewModel.SelectedBook;
            if (SelectedBook == null)
            {
                _messageBoxStore.Show("لطفا کتابی را برای ویرایش انتخاب کنید", "ویرایش کتاب");
            }

            if (_booksViewModel.Name is null || _booksViewModel.Name == "")
            {
                _messageBoxStore.Show("لطفا نام کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksViewModel.Publisher is null || _booksViewModel.Publisher == "")
            {
                _messageBoxStore.Show("لطفا منتشرکننده کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksViewModel.Subject is null || _booksViewModel.Subject == "")
            {
                _messageBoxStore.Show("لطفا نوع کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksViewModel.PublicationDate is null || _booksViewModel.PublicationDate == "")
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            int PublicationYear = 0;
            if (!int.TryParse(_booksViewModel.PublicationDate, out PublicationYear) || _booksViewModel.PublicationDate.Length != 4)
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را عدد 4 رقمی وارد کنید", "افزودن کتاب");
                _booksViewModel.PublicationDate = "";
                return;
            }



            SelectedBook.Name = _booksViewModel.Name;
            SelectedBook.Publisher = _booksViewModel.Publisher;
            SelectedBook.Subject = _booksViewModel.Subject;
            SelectedBook.PublicationDate = _booksViewModel.PublicationDate;
            SelectedBook.Tier = _booksViewModel.Tier;
            await _booksStore.EditBook(SelectedBook);
        }
        #endregion
    }
}
