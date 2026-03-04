using MyLibrary.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public interface IBooksStore
    {
        IEnumerable<Book> Books { get; }
        string SearchBookName { get; set; }
        int SearchSubject { get; set; }

        Book SelectedBook { get; set; }
        int SortIndex
        {
            get; set;
        }
        event Action<Book> BookAdded;
        event Action<Book> BookDeleted;
        event Action<Book> BookEdited;
        event Action BooksUpdated;

        Task AddNewBook(Book book);
        void clear();
        Task DeleteBook(Book book);
        Task EditBook(Book book);
        Task GetAllBooks(string customSql = "");
        Task Load();
    }
}