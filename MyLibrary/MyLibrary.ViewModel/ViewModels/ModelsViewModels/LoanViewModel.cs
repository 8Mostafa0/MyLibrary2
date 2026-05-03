using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using System;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.ModelsViewModels
{
    public class LoanViewModel : ViewModelBase
    {
        #region Properties
        private IMyLibraryDbContext _db;
        public Loan _loan;
        public int ID { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int BookID { get; set; }
        public string BookName { get; set; }
        public string BookSubject { get; set; }

        public DateTime ReturnDate { get; set; }
        public string ReturnedDateTime { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        #endregion

        #region Contructor

        public async Task<LoanViewModel> LoanViewModelAsync(Loan loan, IMyLibraryDbContext db)
        {
            if (!(db is null))
            {

                _loan = loan ?? throw new ArgumentNullException(nameof(loan));

                Client client = await db.ClientsRepository.GetClientById(loan.ClientId);
                ClientName = client != null
                    ? $"{client.FirstName} {client.LastName}".Trim()
                    : $"Client #{loan.ClientId} (not found)";

                Book book = GetLoanBook(loan.BookId).Result;
                BookName = book?.Name ?? $"Book #{loan.BookId} (not found)";

                BookSubject = book?.Subject ?? $"Book #{loan.BookId} (not found)";

                ReturnedDateTime = loan.ReturnedDate.HasValue
                    ? loan.ReturnedDate.Value.ToString("yyyy-MM-dd")
                    : "خیر";
                return new LoanViewModel()
                {
                    BookName = BookName,
                    BookSubject = BookSubject,
                    ClientName = ClientName
                };
            }
            else
            {
                return new LoanViewModel()
                {
                    BookName = "Error",
                    BookSubject = "Error",
                    ClientName = "Error"
                };
            }
        }

        #endregion

        #region Methods

        private async Task<Book> GetLoanBook(int id)
        {
            return await _db.BooksRepository.GetBookById(id);
        }

        private async Task<Client> GetLoanClient(int id)
        {
            return await _db.ClientsRepository.GetClientById(id);
        }

        public Loan ToLoan()
        {
            DateTime? ReturnedDateTime;
            try
            {
                ReturnedDateTime = DateTime.Parse(this.ReturnedDateTime);
            }
            catch
            {
                ReturnedDateTime = null;
            }
            return new Loan()
            {
                Id = ID,
                ClientId = ClientID,
                BookId = BookID,
                ReturnDate = ReturnDate,
                ReturnedDate = ReturnedDateTime,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt
            };
        }
        /// <summary>
        /// Generate Empty Instance of loanViewModel
        /// Book Store is null
        /// Client Store is null
        /// </summary>
        /// <returns></returns>
        public static LoanViewModel Empty()
        {
            LoanViewModel loanViewModel = new LoanViewModel
            {
                ID = 0,
                BookID = 0,
                ClientID = 0
            };
            return loanViewModel;
        }
        #endregion
    }
}
