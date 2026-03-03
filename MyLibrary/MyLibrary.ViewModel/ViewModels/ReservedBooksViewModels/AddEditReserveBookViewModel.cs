using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public class AddEditeReserveBookViewModel : ViewModelBase, IAddEditeReserveBookViewModel
    {
        #region Dependencies
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksStore _reservedBooksStore;
        private IClientsStore _clientsStore;
        private IBooksStore _booksStore;
        private Book _selectedBook;
        private Client _selectedClient;
        private ReservedBook _selectedReservedBook;
        private string _bookName;
        private string _clientName;
        private int _bookSubject;
        private IMessageBoxStore _messageBoxStore;

        private ObservableCollection<Book> _books;
        private ObservableCollection<Client> _clients;

        public IEnumerable<Book> Books => _books;
        public IEnumerable<Client> Clients => _clients;

        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;
                _booksStore.SearchBookName = value;
                OnProperychanged(nameof(BookName));
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                _clientName = value;
                _clientsStore.SearchClientName = value;
                OnProperychanged(nameof(ClientName));
            }
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {

                _selectedBook = value;
                OnProperychanged(nameof(SelectedBook));
            }
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnProperychanged(nameof(SelectedClient));
            }
        }

        public ReservedBook SelectedReservedBook
        {
            get => _selectedReservedBook;
            set
            {
                _selectedReservedBook = value;
                SetDataOfSelectedReservedBook();
            }
        }
        public int BookSubject
        {
            get => _bookSubject;
            set
            {
                _bookSubject = value;
                _booksStore.SearchSubject = value;
                OnProperychanged(nameof(BookSubject));
            }
        }

        public string TitleOfLoanScreen { get; set; }

        #endregion

        #region Commands

        public ICloseModalCommand CloseModalCommand { get; }
        public ICommand SaveReservedBookDataCommand { get; }
        public ICommand SearchBookNameCommand { get; }
        public ICommand SearchClientNameCommand { get; }
        public ILoadClientsCommand LoadClientsCommand { get; }
        public ILoadBooksCommand LoadBooksCommand { get; }
        public ICommand OrderBooksCommand { get; }
        public ICommand OrderBooksBySubjectCommand { get; }


        #endregion

        #region Constructor

        public AddEditeReserveBookViewModel()
        {
            _clients = new ObservableCollection<Client>();
            _books = new ObservableCollection<Book>();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _booksStore = ClassFactory.CreateBooksStore();
            //SelectedReservedBook = reservedBook;
            //if (SelectedReservedBook != null)
            //{
            //    TitleOfLoanScreen = "ویرایش نوبت رزرو";

            //}
            //else
            //{
            //    TitleOfLoanScreen = "رزرو نوبت جدید";
            //}
            _booksStore.BooksUpdated += OnBooksUpdated;
            _clientsStore.ClientsUpdated += OnClientsUpdated;
            LoadBooksCommand = ClassFactory.CreateLoadBooksCommand();
            LoadClientsCommand = ClassFactory.CreateLoadClientsCommand();
            CloseModalCommand = ClassFactory.CreateCloseModalCommand();
            SearchBookNameCommand = new SearchBookNameCommand(_booksStore);
            SearchClientNameCommand = new SearchClientNameCommand(_clientsStore);
            OrderBooksCommand = new OrderBooksBySubjectCommand(_booksStore, _messageBoxStore);
            SaveReservedBookDataCommand = new SaveReservationDataCommand(this, _modalNavigationStore, _reservedBooksStore, loanRepository, reservedBooksRepository, clientsRepository, _messageBoxStore);
            OrderBooksBySubjectCommand = new OrderBooksBySubjectCommand(_booksStore, _messageBoxStore);
        }
        #endregion


        #region Methods

        /// <summary>
        /// get called each time books list of books store get chagned
        /// </summary>
        private void OnBooksUpdated()
        {
            _books.Clear();
            foreach (Book book in _booksStore.Books)
            {
                _books.Add(book);
            }
        }
        /// <summary>
        /// called each time clients list of clients store get chagned
        /// </summary>
        private void OnClientsUpdated()
        {
            _clients.Clear();
            foreach (Client client in _clientsStore.Clients)
            {
                _clients.Add(client);
            }
        }
        /// <summary>
        /// fill data for inputed reservedbooks
        /// </summary>
        private void SetDataOfSelectedReservedBook()
        {
            if (!(_selectedReservedBook is null))
            {
                Book book = _books.SingleOrDefault(b => b.ID == _selectedReservedBook.BookId);
                _selectedBook = book;
                Client client = _clients.SingleOrDefault(c => c.ID == _selectedReservedBook.ClientId);
                _selectedClient = client;
            }
        }
        #endregion

    }
}
