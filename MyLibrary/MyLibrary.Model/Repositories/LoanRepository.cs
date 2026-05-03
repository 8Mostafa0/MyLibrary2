using Dapper;
using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Servicies;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        #region Contructor
        private readonly IDbContextFactory _dbContextFactory;
        private ILogger _logger;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextFactory"></param>
        /// <param name="logger"></param>
        public LoanRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _logger = LoggerService.logger;
        }
        #endregion

        #region Methods
        /// <summary>
        /// default get all loans from Loans Table
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns List<Loan>></returns>
        public async Task<List<Loan>> GetAllLoans(string customSql = "")
        {
            List<Loan> Loans = new List<Loan>();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetClientsSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetClientsSQl = "SELECT * FROM Loans";
                    }
                    else
                    {
                        GetClientsSQl = customSql;
                    }
                    Loans = Connection.Query<Loan>(GetClientsSQl).ToList();
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetAllClients");
                }
                finally
                {
                    Connection.Close();
                }
            }
            return Loans;
        }

        /// <summary>
        /// get selected loan by sql query from database 
        /// </summary>
        /// <param name="customSql"></param>
        /// <param name="executionPart"></param>
        /// <returns Loan></returns>
        public async Task<Loan> GetLoan(string customSql, string executionPart)
        {
            Loan loan = new Loan();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetClientsSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetClientsSQl = "SELECT * FROM Loans LIMIT 1";
                    }
                    else
                    {
                        GetClientsSQl = customSql;
                    }
                    loan = Connection.QuerySingleOrDefault<Loan>(GetClientsSQl);
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetAllClients");
                }
                finally
                {
                    Connection.Close();
                }
            }
            return loan;
        }

        /// <summary>
        /// add new loan to Loans Table
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task<int> AddNewLoanToDb(Loan loan)
        {
            string AddLoanSql = $"INSERT INTO Loans(ClientId,ClientName,BookId,BookName,ReturnDate,ReturnedDate,CreatedAt,UpdatedAt)VALUES('{loan.ClientId}','{loan.ClientName}','{loan.BookId}','{loan.BookName}','{loan.ReturnDate.ToString("O")}',NULL,GETDATE(),GETDATE())";
            Console.WriteLine(AddLoanSql);
            return await _dbContextFactory.ExecuteQueryAsync(AddLoanSql, "AddNewLoan");
        }
        /// <summary>
        /// edite loan data from Loan table
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task UpdateLoanAtDb(Loan loan)
        {
            string UpdateLoanSql = $"UPDATE Loans SET ClientId='{loan.ClientId}',ClientName='{loan.ClientName}',BookId='{loan.BookId}',BookName='{loan.BookName}',ReturnDate='{loan.ReturnDate}',UpdatedAt=GETDATE() WHERE Id='{loan.Id}'";
            await _dbContextFactory.ExecuteQueryAsync(UpdateLoanSql, "DeleteLoanAtDb");
        }

        /// <summary>
        /// delete loan data from Loan Table using loan
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task DeleteLoanInDB(Loan loan)
        {
            string DeleteLoanSql = $"UPDATE Loans SET ReturnedDate=GETDATE() WHERE Id='{loan.Id}'";
            await _dbContextFactory.ExecuteQueryAsync(DeleteLoanSql, "DEleteLoanInDB");
        }

        /// <summary>
        /// get list of dilayed loans from Loans Table
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns List<Loan>></returns>
        public async Task<List<Loan>> UserHaveDilayedLoan(int clientId)
        {
            string SearchSql = $"SELECT * FROM Loans WHERE ClientId='{clientId}' AND ReturnDate < GETDATE() AND ReturnedDate IS NULL";
            return await GetAllLoans(SearchSql);

        }
        /// <summary>
        /// get all current loans of current book by Book Id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns List<Loan>></returns>
        public async Task<List<Loan>> GetNotReturnedLoanOfBook(int bookId)
        {
            string SearchSql = $"SELECT * FROM Loans WHERE BookId='{bookId}' AND ReturnedDate IS NULL";
            return await GetAllLoans(SearchSql);
        }
        /// <summary>
        /// get all client current loan using Client Id from Loans Table
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<List<Loan>> GetAllClientLoans(int clientId)
        {
            string SearchSql = $"SELECT * FROM Loans WHERE ClientId='{clientId}' AND ReturnedDate IS NULL";
            return await GetAllLoans(SearchSql);
        }
        /// <summary>
        /// Remove all client loans using Client Id from Loans Table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveClientLoans(int id)
        {
            string deleteSql = $"DELETE FROM Loans WHERE ClientId='{id}'";
            await _dbContextFactory.ExecuteQueryAsync(deleteSql, "RemoveClientLoans");
        }

        /// <summary>
        /// remove all loans of book by Book Id from Loans
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task RemoveBookLoans(int bookId)
        {
            string deleteSql = $"DELETE FROM Loans WHERE BookId='{bookId}'";
            await _dbContextFactory.ExecuteQueryAsync(deleteSql, "RemoveBookLoans");
        }

        /// <summary>
        /// set ReturnDate of loan to Now
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task SetLoanReturned(Loan loan)
        {
            string DeleteLoanSql = $"UPDATE Loans SET ReturnedDate=GETDATE() WHERE Id='{loan.Id}'";
            await _dbContextFactory.ExecuteQueryAsync(DeleteLoanSql, "DEleteLoanInDB");
        }
        public void Dispose() { }
        #endregion
    }
}
