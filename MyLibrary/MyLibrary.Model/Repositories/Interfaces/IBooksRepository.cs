using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public interface IBooksRepository
    {
        Task<int> AddNewBookToDb(Book book);
        Task<int> DeleteBookInDb(Book book);
        Task<int> EditeBookInDb(Book book);
        Task<List<Book>> GetAllBooks(string customSql = "");
        Task<List<Book>> GetLoanedBooks();
        Task<List<Book>> GetDilayedBook();
        Task<Book> GetBookById(int id);
        Task<List<Book>> GetBooksByName(string bookName);
        void Dispose();
    }
}