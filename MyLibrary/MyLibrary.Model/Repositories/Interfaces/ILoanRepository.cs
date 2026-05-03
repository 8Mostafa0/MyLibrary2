using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public interface ILoanRepository
    {
        Task<int> AddNewLoanToDb(Loan loan);
        Task DeleteLoanInDB(Loan loan);
        Task<List<Loan>> GetAllClientLoans(int clientId);
        Task<List<Loan>> GetAllLoans(string customSql = "");
        Task<Loan> GetLoan(string customSql, string executionPart);
        Task<List<Loan>> GetNotReturnedLoanOfBook(int bookId);
        Task RemoveBookLoans(int bookId);
        Task RemoveClientLoans(int id);
        Task SetLoanReturned(Loan loan);
        Task UpdateLoanAtDb(Loan loan);
        Task<List<Loan>> UserHaveDilayedLoan(int clientId);

        void Dispose();
    }
}