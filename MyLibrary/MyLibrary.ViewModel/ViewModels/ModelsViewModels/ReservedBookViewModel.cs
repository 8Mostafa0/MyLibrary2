using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using System;
using System.Linq;

namespace MyLibrary.ViewModel.ViewModels.ModelsViewModels
{
    public class ReservedBookViewModel : ViewModelBase
    {
        #region Properties
        private ClientsStore _clientsStore;
        private IBooksStore _booksStore;
        private ReservedBook _reservedBook;
        public int ID => _reservedBook.ID;
        public int BookId => _reservedBook.BookId;
        public string BookName { get; set; }
        public int ClientId => _reservedBook.ClientId;
        public string ClientName { get; set; }
        public DateTime CreatedAt => _reservedBook.CreatedAt;
        public DateTime UpdatedAt => _reservedBook.UpdatedAt;
        #endregion

        #region Constructor
        public ReservedBookViewModel(ReservedBook reservedBook, ClientsStore clientsStore, IBooksStore booksStore)
        {
            _reservedBook = reservedBook ?? throw new ArgumentNullException(nameof(reservedBook));
            _clientsStore = clientsStore ?? throw new ArgumentNullException(nameof(clientsStore));
            _booksStore = booksStore ?? throw new ArgumentNullException(nameof(booksStore));

            var client = clientsStore.Clients.FirstOrDefault(c => c.ID == reservedBook.ClientId);
            ClientName = client != null
                ? $"{client.FirstName} {client.LastName}".Trim()
                : $"Client #{reservedBook.ClientId} (not found)";

            var book = booksStore.Books.FirstOrDefault(b => b.ID == reservedBook.BookId);
            BookName = book?.Name ?? $"Book #{reservedBook.BookId} (not found)";
        }
        #endregion

        #region Methods

        public ReservedBook ToReservedBook()
        {
            return _reservedBook;
        }
        #endregion
    }
}
