using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using System;
using System.Linq;

namespace MyLibrary.ViewModel.ViewModels.ModelsViewModels
{
    public class LoanViewModel : ViewModelBase
    {
        #region Properties
        private ClientsStore _clientsStore;
        private BooksStore _booksStore;
        public Loan _loan;
        public int ID => _loan.Id;
        public int ClientID => _loan.ClientId;
        public string ClientName { get; }
        public int BookID => _loan.BookId;
        public string BookName { get; }
        public string BookSubject { get; }

        public DateTime ReturnDate => _loan.ReturnDate;
        public string ReturnedDateTime { get; set; }

        public DateTime CreatedAt => _loan.CreatedAt;
        public DateTime UpdatedAt => _loan.UpdatedAt;

        #endregion

        #region Contructor

        public LoanViewModel(Loan loan, ClientsStore clientsStore, BooksStore booksStore)
        {
            if (!(clientsStore is null) && !(booksStore is null))
            {

                _loan = loan ?? throw new ArgumentNullException(nameof(loan));
                _clientsStore = clientsStore ?? throw new ArgumentNullException(nameof(clientsStore));
                _booksStore = booksStore ?? throw new ArgumentNullException(nameof(booksStore));

                var client = clientsStore.Clients.FirstOrDefault(c => c.ID == loan.ClientId);
                ClientName = client != null
                    ? $"{client.FirstName} {client.LastName}".Trim()
                    : $"Client #{loan.ClientId} (not found)";

                var book = booksStore.Books.FirstOrDefault(b => b.ID == loan.BookId);
                BookName = book?.Name ?? $"Book #{loan.BookId} (not found)";

                BookSubject = book?.Subject ?? $"Book #{loan.BookId} (not found)";

                ReturnedDateTime = loan.ReturnedDate.HasValue
                    ? loan.ReturnedDate.Value.ToString("yyyy-MM-dd")
                    : "خیر";
            }
        }
        #endregion

        #region Methods
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
            return new LoanViewModel(new Loan() { Id = 0, BookId = 0, ClientId = 0 }, null, null);
        }
        #endregion
    }
}
