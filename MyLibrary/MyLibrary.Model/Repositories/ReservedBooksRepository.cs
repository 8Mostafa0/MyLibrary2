using Dapper;
using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Models;
using Serilog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public class ReservedBooksRepository : IReservedBooksRepository
    {
        #region Dependencies
        private IDbContextFactory _dbContextFactory;
        private ILogger _logger;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        public ReservedBooksRepository(IDbContextFactory dbContextFactory, ILogger logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }
        #endregion


        #region Methods
        /// <summary>
        /// get all reserved books by sql query from ReservedBooks table
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns >List<ReservedBook></returns>
        public async Task<List<ReservedBook>> GetAllReservedBooks(string customSql = "")
        {
            List<ReservedBook> ReservedBooks = new List<ReservedBook>();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetReservedBooksSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetReservedBooksSQl = "SELECT * FROM ReservedBooks";
                    }
                    else
                    {
                        GetReservedBooksSQl = customSql;
                    }
                    ReservedBooks = Connection.Query<ReservedBook>(GetReservedBooksSQl).ToList();
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetAllReservedBooks");
                }
                finally
                {
                    Connection.Close();
                }
                return ReservedBooks;
            }
        }

        /// <summary>
        /// select single reservation from ReservedBooks Table by sql query
        /// </summary>
        /// <param name="customSql"></param>
        /// <param name="executionPart"></param>
        /// <returns ReservedBook></returns>
        public async Task<ReservedBook> GetReservedBook(string customSql, string executionPart)
        {
            ReservedBook ReservedBook = new ReservedBook();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetReservedBookSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetReservedBookSQl = $"SELECT * FROM ReservedBooks LIMIT 1";
                    }
                    else
                    {
                        GetReservedBookSQl = customSql;
                    }
                    ReservedBook = Connection.QueryFirstOrDefault<ReservedBook>(GetReservedBookSQl);
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetReservedBookById", executionPart);
                }
                finally
                {
                    Connection.Close();
                }
                return ReservedBook;
            }

        }

        /// <summary>
        /// add new Reservation to ReservedBooks Table
        /// </summary>
        /// <param name="reservedBook"></param>
        /// <returns></returns>
        public async Task AddNewReservedBookToDb(ReservedBook reservedBook)
        {
            string AddReservSql = $"INSERT INTO ReservedBooks(BookId,ClientId,CreatedAt,UpdatedAt)VALUES('{reservedBook.BookId}','{reservedBook.ClientId}',GETDATE(),GETDATE())";
            await _dbContextFactory.ExecuteQueryAsync(AddReservSql, "AddNewReservedBook");
        }
        /// <summary>
        /// edite reservation data from ReservedBook Table
        /// </summary>
        /// <param name="reservedBook"></param>
        /// <returns></returns>
        public async Task EditReservBookToDb(ReservedBook reservedBook)
        {
            string EditeReservSql = $"UPDATE ReservedBooks SET BookId='{reservedBook.BookId}',ClientId='{reservedBook.ClientId}',UpdatedAt=GETDATE() WHERE Id='{reservedBook.ID}'";
            await _dbContextFactory.ExecuteQueryAsync(EditeReservSql, "EditReservBookToDb");
        }
        /// <summary>
        /// delete reservation dedicated to book and client from ReservedBooks Table
        /// </summary>
        /// <param name="reservedBook"></param>
        /// <returns></returns>
        public async Task DeleteReservedBookWithClientToDb(ReservedBook reservedBook)
        {
            string DeleteReservSql = $"DELETE FROM ReservedBooks WHERE Id='{reservedBook.ID}' AND ClientId='{reservedBook.ClientId}'";
            await _dbContextFactory.ExecuteQueryAsync(DeleteReservSql, "DeleteReservedBook");
        }
        /// <summary>
        ///Check dose user have reservation and if so return ir
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>ReservedBook?</returns>
        public async Task<ReservedBook> UserHaveReservedBook(int clientId)
        {
            string SearchSql = $"SELECT * FROM ReservedBooks WHERE ClientId ='{clientId}'";
            return await GetReservedBook(SearchSql, "UserHaveReservedBook");
        }
        /// <summary>
        /// check book is reserved or not 
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns>ReservedBook?</returns>
        public async Task<ReservedBook> BookAlreadyRegistred(int bookId)
        {
            string SearchSql = $"SELECT * FROM ReservedBooks WHERE BookId='{bookId}'";
            return await GetReservedBook(SearchSql, "UserHaveReservedBook");
        }
        /// <summary>
        /// get all reservation for a book by Book Id from ReservedBooks Table
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<List<ReservedBook>> GetReservationForBook(int bookId)
        {
            string SearchSql = $"SELECT * FROM ReservedBooks WHERE BookId='{bookId}'";
            return await GetAllReservedBooks(SearchSql);
        }
        /// <summary>
        /// Remove all client Reservation by Client Id from ReservedBooks Table
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task RemoveClientReservedBooks(int clientId)
        {
            string deleteSql = $"DELETE FROM ReservedBooks WHERE ClientId='{clientId}'";
            await _dbContextFactory.ExecuteQueryAsync(deleteSql, "RemoveUserReservedBooks");
        }

        /// <summary>
        /// Delete all reservation fo book by Book Id from ReservedBooks Table
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task DeleteReservedBookToDb(int bookId)
        {
            string DeleteReservSql = $"DELETE FROM ReservedBooks WHERE BookId='{bookId}'";
            await _dbContextFactory.ExecuteQueryAsync(DeleteReservSql, "DeleteReservedBook");
        }
        #endregion
    }
}
