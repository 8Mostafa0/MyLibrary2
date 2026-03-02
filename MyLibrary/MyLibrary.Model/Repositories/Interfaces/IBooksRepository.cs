using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public interface IBooksRepository
    {
        Task AddNewBookToDb(Book book);
        Task DeleteBookInDb(Book book);
        Task EditeBookInDb(Book book);
        Task<List<Book>> GetAllBooks(string customSql = "");
        Task<Book> GetBookById(int id, string customSql);
        Task<List<Book>> GetBooksByName(string bookName);
    }
}