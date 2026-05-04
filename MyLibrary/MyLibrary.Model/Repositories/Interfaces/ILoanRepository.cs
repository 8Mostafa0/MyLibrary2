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
        Task<List<Loan>> GetNotReturnedLoans();
        Task<List<Loan>> GetDilayedLoans();
        Task<List<Loan>> GetReturnedLoans();

        Task RemoveBookLoans(int bookId);
        Task RemoveClientLoans(int id);
        Task<int> SetLoanReturned(Loan loan);
        Task<int> UpdateLoanAtDb(Loan loan);
        Task<List<Loan>> UserHaveDilayedLoan(int clientId);
        Task<List<Loan>> GetLoansByBookName(string bookName);
        void Dispose();
    }
}