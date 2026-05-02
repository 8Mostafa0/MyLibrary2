using Dapper;
using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Servicies;
using Serilog;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Model.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        #region Dependencies
        private IDbContextFactory _dbContextFactory;
        private ILogger _logger;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextFactory"></param>
        /// <param name="logger"></param>
        public BooksRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _logger = LoggerService.logger;
        }
        #endregion


        #region Methods
        /// <summary>
        /// query to get books from database
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns List<Book>> Generic List Of Selecte Books</returns>
        public async Task<List<Book>> GetAllBooks(string customSql = "")
        {
            List<Book> Books = new List<Book>();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetBooksSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetBooksSQl = "SELECT * FROM Books";
                    }
                    else
                    {
                        GetBooksSQl = customSql;
                    }
                    Books = Connection.Query<Book>(GetBooksSQl).ToList();
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetAllBooks");
                }
                finally
                {
                    Connection.Close();
                }
                return Books;
            }
        }

        /// <summary>
        /// Select book by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customSql"></param>
        /// <returns Book></returns>
        public async Task<Book> GetBookById(int id, string customSql)
        {
            Book Book = new Book();
            using (SqlConnection Connection = _dbContextFactory.GetConnection())
            {
                try
                {
                    string GetBookSQl = "";
                    if (string.IsNullOrEmpty(customSql))
                    {
                        GetBookSQl = $"SELECT * FROM Books WHERE Id='{id}'";
                    }
                    else
                    {
                        GetBookSQl = customSql;
                    }
                    Book = Connection.QuerySingle<Book>(GetBookSQl);
                }
                catch (SqlException e)
                {
                    _logger.Warning(e, "GetBookById");
                }
                finally
                {
                    Connection.Close();
                }
                return Book;
            }

        }

        /// <summary>
        /// insert new book into Books Table
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task AddNewBookToDb(Book book)
        {
            string NewBookSql = $"INSERT INTO Books(Name,Publisher,Subject,PublicationDate,Tier,CreatedAt,UpdatedAt)VALUES(N'{book.Name}',N'{book.Publisher}',N'{book.Subject}','{book.PublicationDate}','{book.Tier}',GETDATE(),GETDATE())";
            await _dbContextFactory.ExecuteQueryAsync(NewBookSql, "AddNewBookToDb");
        }

        /// <summary>
        /// edite input book data to Books Table
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task EditeBookInDb(Book book)
        {
            string EditeSql = $"UPDATE books SET Name=N'{book.Name}',Subject=N'{book.Subject}',Publisher=N'{book.Publisher}',PublicationDate='{book.PublicationDate}',Tier='{book.Tier}',UpdatedAt=GETDATE() WHERE Id='{book.ID}'";
            await _dbContextFactory.ExecuteQueryAsync(EditeSql, "EditeBookInDb");
        }

        /// <summary>
        /// delete book by Id from Books Table
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task DeleteBookInDb(Book book)
        {
            string DeleteSql = $"DELETE FROM books WHERE ID='{book.ID}'";
            await _dbContextFactory.ExecuteQueryAsync(DeleteSql, "DeleteBookInDb");
        }

        /// <summary>
        /// get list of generc from books searched by name
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns List<Book>></returns>
        public async Task<List<Book>> GetBooksByName(string bookName)
        {
            string SearchSql = $"SELECT * FROM Books WHERE Name=N'{bookName}'";
            return await GetAllBooks(SearchSql);
        }

        public void Dispose() { }

        #endregion
    }
}
