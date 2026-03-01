using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public class BooksViewModel : ViewModelBase
    {
        #region Dependencies
        private BooksStore _booksStore;
        private ObservableCollection<Book> _books;
        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (value != null)
                {

                    _selectedBook = value;
                    Name = value.Name;
                    Subject = value.Subject;
                    Publisher = value.Publisher;
                    PublicationDate = value.PublicationDate;
                    Tier = value.Tier;
                }

                OnProperychanged(nameof(SelectedBook));
            }
        }
        public IEnumerable<Book> Books => _books;
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnProperychanged(nameof(Name));
            }
        }
        private string _publisher;
        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnProperychanged(nameof(Publisher));
            }
        }

        private string _subject;
        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnProperychanged(nameof(Subject));
            }
        }

        private string _publicationDate;
        public string PublicationDate
        {
            get => _publicationDate;
            set
            {
                _publicationDate = value;
                OnProperychanged(nameof(PublicationDate));
            }
        }

        private int _tier;
        public int Tier
        {
            get => _tier;
            set
            {
                _tier = value;
                OnProperychanged(nameof(Tier));
            }
        }

        private int _sortIndex;
        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                _sortIndex = value;
                OnProperychanged(nameof(SortIndex));
            }
        }
        #endregion

        #region Commands
        public ICommand LoadBooksCommand { get; }
        public ICommand AddNewBookCommand { get; }

        public ICommand DeleteBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand OrderBooksCommand { get; }

        public ICommand ReloadClientsCommand { get; }

        #endregion

        #region Constructor
        public BooksViewModel(BooksStore booksStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository, BooksRepository booksRepository)
        {
            _booksStore = booksStore;
            _books = new ObservableCollection<Book>();
            LoadBooksCommand = new LoadBooksCommand(_booksStore);
            AddNewBookCommand = new AddNewBookCommand(_booksStore, this, booksRepository);
            EditBookCommand = new EditBookCommand(this, _booksStore, booksRepository);
            DeleteBookCommand = new DeleteBookCommand(this, _booksStore, loanRepository, reservedBooksRepository);
            OrderBooksCommand = new OrderBooksByStateCommand(this, _booksStore);
            ReloadClientsCommand = new ReloadBooksCommand(_booksStore);
            _booksStore.BooksUpdated += UpdateBooks;
            _booksStore.BookEdited += BookEdited;
            _booksStore.BookAdded += AddNewBook;
            _booksStore.BookDeleted += BookDeleted;
            Subject = "رمان";
        }
        #endregion

        #region Methods


        /// <summary>
        /// Clear all inouts and set default values
        /// </summary>
        private void ClearInputs()
        {
            Name = "";
            Publisher = "";
            Subject = "رمان";
            PublicationDate = "";
            Tier = 0;
        }
        /// <summary>
        /// called each time a book delete event get trigered and delete book from books list
        /// </summary>
        /// <param name="book"></param>
        private void BookDeleted(Book book)
        {
            ClearInputs();
            _books.Remove(book);
            //MessageBox.Show("کتاب با موفقیت حذف شد", "حذف کتاب");
        }

        /// <summary>
        /// called each time a book edited event trigred and update it in the books list
        /// </summary>
        /// <param name="book"></param>
        private void BookEdited(Book book)
        {
            ClearInputs();
            int index = _books.IndexOf(book);
            _books[index] = book;
            //MessageBox.Show("کتاب با موفقیت ویرایش شد", "ویرایش کتاب");
        }

        /// <summary>
        /// called each time books list in store get changed and update books list
        /// </summary>
        public void UpdateBooks()
        {
            ClearInputs();
            _books.Clear();
            foreach (Book book in _booksStore.Books)
            {
                _books.Add(book);
            }
        }
        /// <summary>
        /// get called each time a new book event trigred and add new book to books list
        /// </summary>
        /// <param name="book"></param>
        public void AddNewBook(Book book)
        {
            ClearInputs();
            book.ID = _books.Any() ? _books.Last().ID + 1 : 1;
            _books.Add(book);
            //MessageBox.Show("کتاب با موفقیت افزوده شد", "افزودن کتاب");
        }
        /// <summary>
        /// Loader Method for Books view model
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="booksRepository"></param>
        /// <returns></returns>
        public static BooksViewModel LoadViewModel(BooksStore booksStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository, BooksRepository booksRepository)
        {
            BooksViewModel ViewModel = new BooksViewModel(booksStore, loanRepository, reservedBooksRepository, booksRepository);
            ViewModel.LoadBooksCommand.Execute(null);
            return ViewModel;
        }
        #endregion
    }
}
