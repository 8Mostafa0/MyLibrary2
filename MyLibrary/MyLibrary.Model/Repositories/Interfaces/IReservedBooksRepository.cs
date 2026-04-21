using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public interface IReservedBooksRepository
    {
        Task AddNewReservedBookToDb(ReservedBook reservedBook);
        Task<ReservedBook> BookAlreadyRegistred(int bookId);
        Task DeleteReservedBookToDb(int bookId);
        Task DeleteReservedBookWithClientToDb(ReservedBook reservedBook);
        Task EditReservBookToDb(ReservedBook reservedBook);
        Task<List<ReservedBook>> GetAllReservedBooks(string customSql = "");
        Task<List<ReservedBook>> GetReservationForBook(int bookId);
        Task<ReservedBook> GetReservedBook(string customSql, string executionPart);
        Task RemoveClientReservedBooks(int clientId);
        Task<ReservedBook> UserHaveReservedBook(int clientId);

        void Dispose();
    }
}