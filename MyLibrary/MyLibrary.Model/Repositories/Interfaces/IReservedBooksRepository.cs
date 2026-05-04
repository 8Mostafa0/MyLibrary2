using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public interface IReservedBooksRepository
    {
        Task<int> AddNewReservedBookToDb(ReservedBook reservedBook);
        Task<ReservedBook> BookAlreadyRegistred(int bookId);
        Task<int> DeleteReservedBookToDb(int bookId);
        Task<int> DeleteReservedBookWithClientToDb(ReservedBook reservedBook);
        Task<int> EditReservBookToDb(ReservedBook reservedBook);
        Task<List<ReservedBook>> GetAllReservedBooks(string customSql = "");
        Task<List<ReservedBook>> GetReservationForBook(int bookId);
        Task<ReservedBook> GetReservedBook(string customSql, string executionPart);
        Task<int> RemoveClientReservedBooks(int clientId);
        Task<ReservedBook> UserHaveReservedBook(int clientId);
        Task<List<ReservedBook>> SearchBookNameInReservedBooks(string bookName);
        void Dispose();
    }
}