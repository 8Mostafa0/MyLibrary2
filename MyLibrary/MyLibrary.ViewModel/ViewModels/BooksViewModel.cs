using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels
{
    public class BooksViewModel : PropertyChangedBase, IBooksViewModel
    {
        #region Dependencies
        private ObservableCollection<Book> _books;
        private IMyLibraryDbContext _db;
        private Book _selectedBook;
        private IMessageBoxStore _messageBoxStore;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (value != null)
                {

                    Name = value.Name;
                    Subject = value.Subject;
                    Publisher = value.Publisher;
                    Tier = value.Tier;
                    PublicationDate = value.PublicationDate;
                    SetField(ref _selectedBook, value);
                    OnBookDataChanged();
                }
            }
        }
        public IEnumerable<Book> Books => _books;
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetField(ref _name, value);
                OnBookDataChanged();

            }
        }
        private string _publisher;
        public string Publisher
        {
            get => _publisher;
            set
            {
                SetField(ref _publisher, value);
                OnBookDataChanged();
            }
        }

        private string _subject;
        public string Subject
        {
            get => _subject;
            set
            {
                SetField(ref _subject, value);
                OnBookDataChanged();
            }
        }

        private string _publicationDate;
        public string PublicationDate
        {
            get => _publicationDate;
            set
            {
                SetField(ref _publicationDate, value);
                OnBookDataChanged();
            }
        }

        private int _tier;
        public int Tier
        {
            get => _tier;
            set
            {
                SetField(ref _tier, value);
                OnBookDataChanged();
            }
        }

        private int _sortIndex;
        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                SetField(ref _sortIndex, value);
            }
        }
        #endregion

        #region Commands
        public AsyncRelayCommand AddNewBookCommand { get; }

        public AsyncRelayCommand DeleteBookCommand { get; }
        public AsyncRelayCommand EditBookCommand { get; }
        public AsyncRelayCommand OrderBooksCommand { get; }

        public AsyncRelayCommand ReloadBooksCommand { get; }

        #endregion

        #region Constructor
        public BooksViewModel(
            IMyLibraryDbContext db,
            IMessageBoxStore messageBoxStore
            )
        {
            _db = db;
            _books = new ObservableCollection<Book>();

            _messageBoxStore = messageBoxStore;
            AddNewBookCommand = new AsyncRelayCommand(AddNewBook, ValidateInputData);
            EditBookCommand = new AsyncRelayCommand(EditeBook, () => { return ValidateInputData() && !(SelectedBook is null); });
            DeleteBookCommand = new AsyncRelayCommand(DeleteBook, () => { return !(SelectedBook is null); });
            ReloadBooksCommand = new AsyncRelayCommand(RefreshPage);
            OrderBooksCommand = new AsyncRelayCommand(ChangeSortOrder);

            RefreshPage();
        }
        #endregion

        #region Methods


        /// <summary>
        /// change order of books list based on Sortindex
        /// </summary>
        /// <returns></returns>
        private async Task ChangeSortOrder()
        {
            ClearInputs();
            _books.Clear();
            List<Book> books = new List<Book>();

            /// 0 همه کتاب ها
            /// 1 امانت برد ها
            /// 2 دیرکرد ها

            switch (SortIndex)
            {
                case 0: books = await _db.BooksRepository.GetAllBooks(); break;
                case 1: books = await _db.BooksRepository.GetLoanedBooks(); break;
                case 2: books = await _db.BooksRepository.GetDilayedBook(); break;
            }

            foreach (Book book in books)
            {
                _books.Add(book);
            }
            ClearInputs();
        }

        /// <summary>
        /// triger OnCanExecute event of commands
        /// </summary>
        private void OnBookDataChanged()
        {
            AddNewBookCommand.RaiseCanExecuteChanged();
            EditBookCommand.RaiseCanExecuteChanged();
            DeleteBookCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// check that data is valid or not for executing commands
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputData()
        {
            if (string.IsNullOrEmpty(Name)) return false;
            if (string.IsNullOrEmpty(Publisher)) return false;
            if (string.IsNullOrEmpty(Subject)) return false;
            if (Tier < 0) return false;
            if (string.IsNullOrEmpty(PublicationDate)) return false;
            return true;
        }

        /// <summary>
        /// Clear all inouts and set default values
        /// </summary>
        private void ClearInputs()
        {
            Name = string.Empty;
            Publisher = string.Empty;
            Subject = "رمان";
            PublicationDate = string.Empty;
            Tier = 0;
            SelectedBook = null;
        }
        /// <summary>
        /// called each time a book delete event get trigered and delete book from books list
        /// </summary>
        /// <param name="book"></param>
        private async Task DeleteBook()
        {
            if (SelectedBook == null)
            {
                _messageBoxStore.Show("لطفا ابتدا کتابی را انتخاب کنید", "حذف کتاب");
                return;
            }
            ClearInputs();
            int result = await _db.BooksRepository.DeleteBookInDb(SelectedBook);
            if (result > 0)
            {
                _messageBoxStore.Show("کتاب با موفقیت حذف شد", "حذف کتاب");
                RefreshPage();
            }
            else
            {
                _messageBoxStore.Show("هنگام حذف کتاب مشکلی بوجود امده است", "حذف کتاب");
            }
        }

        /// <summary>
        /// called each time a book edited event trigred and update it in the books list
        /// </summary>
        /// <param name="book"></param>
        private async Task EditeBook()
        {
            if (SelectedBook == null)
            {
                _messageBoxStore.Show("لطفا ابتدا کتابی را برای ویرایش انتخاب کنید", "ویرایش کتاب");
                return;
            }
            SelectedBook.Name = Name;
            SelectedBook.Publisher = Publisher;
            SelectedBook.Subject = Subject;
            SelectedBook.Tier = Tier;
            SelectedBook.PublicationDate = PublicationDate;
            int result = await _db.BooksRepository.EditeBookInDb(SelectedBook);
            if (result > 0)
            {
                _messageBoxStore.Show("ویرایش کتاب با موفقیت انجام شد", "ویرایش کتاب");
                RefreshPage();
            }
            else
            {
                _messageBoxStore.Show("هنگام ویرایش کتاب مشکلی بوجود امده است", "ویرایش کتاب");
            }
        }

        /// <summary>
        /// called each time books list in store get changed and update books list
        /// </summary>
        public async Task RefreshPage()
        {
            ClearInputs();
            _books.Clear();
            foreach (Book book in await _db.BooksRepository.GetAllBooks())
            {
                _books.Add(book);
            }
        }
        /// <summary>
        /// get called each time a new book event trigred and add new book to books list
        /// </summary>
        public async Task AddNewBook()
        {
            Book newBook = new Book()
            {
                Name = _name,
                Subject = _subject,
                Publisher = _publisher,
                PublicationDate = _publicationDate,
                Tier = _tier
            };
            int result = await _db.BooksRepository.AddNewBookToDb(newBook);
            if (result > 0)
            {
                _messageBoxStore.Show("کتاب جدید با موفقیت افزوده شد", "افزودن کتاب");
                RefreshPage();
            }
            else
            {
                _messageBoxStore.Show("هنگام افزودن کتاب جدید مشکلی بوجود امده است", "افزودن کتاب");
            }
        }
        #endregion
    }
}
