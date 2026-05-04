using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public class AddEditeReserveBookViewModel : PropertyChangedBase, IAddEditeReserveBookViewModel
    {
        #region Dependencies
        private IMyLibraryDbContext _db;
        private IApplicationStore _applicationStore;

        private Book _selectedBook;
        private Client _selectedClient;

        private IMessageBoxStore _messageBoxStore;
        private ReservedBook _selectedReservedBook;
        private IModalNavigationStore _modalNavigationStore;

        private ObservableCollection<Book> _books;
        private ObservableCollection<Client> _clients;

        private string _bookName;
        private string _clientName;
        private int _bookSubject;


        public IEnumerable<Book> Books => _books;
        public IEnumerable<Client> Clients => _clients;

        public string BookName
        {
            get => _bookName;
            set
            {
                SetField(ref _bookName, value);
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                SetField(ref _clientName, value);
            }
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                SetField(ref _selectedBook, value);
                SaveReservedBookDataCommand.RaiseCanExecuteChanged();
            }
        }

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                SetField(ref _selectedClient, value);
                SaveReservedBookDataCommand.RaiseCanExecuteChanged();
            }
        }

        public ReservedBook SelectedReservedBook
        {
            get => _selectedReservedBook;
            set
            {
                SetField(ref _selectedReservedBook, value);
                SetDataOfSelectedReservedBook();
            }
        }
        public int BookSubject
        {
            get => _bookSubject;
            set
            {
                SetField(ref _bookSubject, value);
            }
        }

        public string TitleOfLoanScreen { get; set; }

        #endregion

        #region Commands

        public RelayCommand CloseModalCommand { get; }
        public AsyncRelayCommand SaveReservedBookDataCommand { get; }
        public AsyncRelayCommand SearchBookNameCommand { get; }
        public AsyncRelayCommand SearchClientNameCommand { get; }
        public AsyncRelayCommand OrderBooksBySubjectCommand { get; }


        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="applicationStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="modalNavigationStore"></param>
        public AddEditeReserveBookViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore

            )
        {
            _db = db;
            _applicationStore = applicationStore;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;

            _clients = new ObservableCollection<Client>();
            _books = new ObservableCollection<Book>();

            TitleOfLoanScreen = "رزرو نوبت جدید";
            CloseModalCommand = new RelayCommand(CloseModal);
            SearchBookNameCommand = new AsyncRelayCommand(SearchBookName, () => { return !string.IsNullOrEmpty(BookName); });
            SearchClientNameCommand = new AsyncRelayCommand(SearchClientName, () => { return !string.IsNullOrEmpty(ClientName); });
            OrderBooksBySubjectCommand = new AsyncRelayCommand(OrderBooksBySubject, ValidateReservdBookData);
            SaveReservedBookDataCommand = new AsyncRelayCommand(SaveReservedBooksData, ValidateReservdBookData);
        }
        #endregion


        #region Methods

        private bool ValidateReservdBookData()
        {
            if (SelectedBook is null) return false;
            if (SelectedClient is null) return false;
            return true;
        }

        private async Task SaveReservedBooksData()
        {

            if (SelectedReservedBook.ID == ReservedBook.Empty().ID)
            {
                ReservedBook reservBook = new ReservedBook()
                {
                    BookId = SelectedBook.ID,
                    BookName = SelectedBook.Name,
                    ClientId = SelectedClient.ID,
                    ClientName = SelectedClient.FirstName + " " + SelectedClient.LastName
                };

                int result = await _db.ReservedBooksRepository.AddNewReservedBookToDb(reservBook);
                if (result > 0)
                {
                    _messageBoxStore.Show("رزرو کتاب با موفقیت انجام شد", "رزرو کتاب");
                    CloseModal();
                }
                else
                {

                    _messageBoxStore.Show("هنگام رزرو کتاب مشکلی بوجود امده است", "رزرو کتاب");
                }
            }
            else
            {
                SelectedReservedBook.BookId = SelectedBook.ID;
                SelectedReservedBook.BookName = SelectedBook.Name;
                SelectedReservedBook.ClientId = SelectedClient.ID;
                SelectedReservedBook.ClientName = SelectedClient.FirstName + " " + SelectedClient.LastName;
                int result = await _db.ReservedBooksRepository.EditReservBookToDb(SelectedReservedBook);
                if (result > 0)
                {
                    _messageBoxStore.Show("ویرایش با موفقیت انجام شد", "ویرایش رزرو کتاب");
                    CloseModal();
                }
                else
                {
                    _messageBoxStore.Show("هنگام ویرایش رزرو مشکلی بوحود امده است", "ویرایش رزرو کتاب");
                }
            }
        }

        /// <summary>
        /// get list of books by subject
        /// </summary>
        /// <returns></returns>
        private async Task OrderBooksBySubject()
        {
            List<Book> books = await _db.BooksRepository.GetBoooksBySubject(BookSubject);
            _books.Clear();
            foreach (Book book in books)
            {
                _books.Add(book);
            }
        }

        /// <summary>
        /// Get All Clients which name of them contain the ClientName string
        /// </summary>
        /// <returns></returns>
        private async Task SearchClientName()
        {
            List<Client> clientes = await _db.ClientsRepository.GetClientsByName(ClientName);
            _clients.Clear();
            foreach (Client client in clientes)
            {

                _clients.Add(client);
            }
        }

        /// <summary>
        /// Get Books that contain BookName string
        /// </summary>
        /// <returns></returns>
        private async Task SearchBookName()
        {
            List<Book> books = await _db.BooksRepository.GetBooksByName(BookName);
            _books.Clear();
            foreach (Book book in books)
            {
                _books.Add(book);
            }
        }


        /// <summary>
        /// Initialize ViewModel in the async way
        /// </summary>
        /// <param name="db"></param>
        /// <param name="applicationStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <returns></returns>
        public static async Task<AddEditeReserveBookViewModel> InitializeViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore)
        {
            AddEditeReserveBookViewModel viewModel = new AddEditeReserveBookViewModel(db, applicationStore, messageBoxStore, modalNavigationStore);
            await viewModel.RefreshPage();
            return viewModel;
        }



        private void CloseModal()
        {
            _modalNavigationStore.Close();
        }

        /// <summary>
        /// Refresh the page data
        /// </summary>
        /// <returns></returns>
        private async Task RefreshPage()
        {
            await LoadBooks();
            await LoadClients();
        }

        /// <summary>
        /// load all books
        /// </summary>
        /// <returns></returns>
        private async Task LoadBooks()
        {
            List<Book> books = await _db.BooksRepository.GetAllBooks();
            _books.Clear();
            foreach (Book book in books)
            {
                _books.Add(book);
            }
        }
        /// <summary>
        /// load all clients
        /// </summary>
        /// <returns></returns>
        private async Task LoadClients()
        {
            List<Client> clients = await _db.ClientsRepository.GetAllClients();
            _clients.Clear();
            foreach (Client client in clients)
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
                TitleOfLoanScreen = "ویرایش نوبت رزرو";
                Book book = _books.SingleOrDefault(b => b.ID == _selectedReservedBook.BookId);
                SelectedBook = book;
                Client client = _clients.SingleOrDefault(c => c.ID == _selectedReservedBook.ClientId);
                SelectedClient = client;
            }
        }
        #endregion

    }
}
