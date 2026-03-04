using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{

    public class BooksStore : IBooksStore
    {
        #region Dependencies
        private List<Book> _books;
        private IBooksRepository _booksRepository;
        public IEnumerable<Book> Books => _books;

        public event Action BooksUpdated;
        public Lazy<Task> _initilizerLazy;
        public event Action<Book> BookAdded;
        public event Action<Book> BookEdited;
        public event Action<Book> BookDeleted;

        public string SearchBookName { get; set; }
        public Book SelectedBook { get; set; } = new Book();
        public int SortIndex { get; set; }
        public int SearchSubject { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksRepository"></param>
        public BooksStore(IBooksRepository booksRepository)
        {
            _books = new List<Book>();
            _initilizerLazy = new Lazy<Task>(Initilize);
            _booksRepository = booksRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get values for first time and the next time using value in the memory
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            await _initilizerLazy.Value;
        }
        /// <summary>
        /// call for delete method in database and invoke event of delete book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task DeleteBook(Book book)
        {
            await _booksRepository.DeleteBookInDb(book);
            BookDeleted?.Invoke(book);
        }

        /// <summary>
        /// call for edite method in databse and invoke event of edite book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task EditBook(Book book)
        {
            await _booksRepository.EditeBookInDb(book);
            BookEdited?.Invoke(book);

        }
        /// <summary>
        /// call for add mthod in database and invoke event of add book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task AddNewBook(Book book)
        {
            await _booksRepository.AddNewBookToDb(book);
            _books.Add(book);
            BookAdded?.Invoke(book);
        }

        /// <summary>
        /// get all book from database method and clear all books store in store and fill it with new values
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns></returns>
        public async Task GetAllBooks(string customSql = "")
        {
            List<Book> books = await _booksRepository.GetAllBooks(customSql);
            _books.Clear();
            _books.AddRange(books);
            BooksUpdated?.Invoke();
        }

        /// <summary>
        /// initilize books store value for first time
        /// </summary>
        /// <returns></returns>
        private async Task Initilize()
        {
            IEnumerable<Book> books = await _booksRepository.GetAllBooks();
            _books.Clear();
            _books.AddRange(books);
            BooksUpdated?.Invoke();
        }
        /// <summary>
        /// clear all books store in memory
        /// </summary>
        public void clear()
        {
            _books.Clear();
            BooksUpdated?.Invoke();
        }
        #endregion
    }
}
