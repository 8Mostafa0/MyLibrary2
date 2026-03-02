using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public interface IBooksStore
    {
        IEnumerable<Book> Books { get; }
        string SearchBookName { get; set; }
        int SearchSubject { get; set; }

        Task AddNewBook(Book book);
        void clear();
        Task DeleteBook(Book book);
        Task EditBook(Book book);
        Task GetAllBooks(string customSql = "");
        Task Load();
    }
}