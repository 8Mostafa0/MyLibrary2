using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public class AddEditeLoanViewModel : ViewModelBase, IAddEditeLoanViewModel
    {

        #region Dependencies
        private Loan _selectedLoan;
        private ClientsStore _clientsStore;
        private BooksStore _booksStore;
        private ObservableCollection<Book> _books;
        private ObservableCollection<Client> _clients;
        private Client _seletedClient;
        private Book _selectedBook;
        private LoansStore _loansStore;
        private LoanRepository _loanRepository;
        private ModalNavigationStore _modalNavigationStore;
        private SettingsStore _settingsStore;
        private BooksRepository _booksRepository;
        private ReservedBooksRepository _reservedBooksRepository;
        private MessageBoxStore _messageBoxStore;
        private string _titleOfLoanScreen;

        public Loan SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                _selectedLoan = value;
                SetDataOfSelectedReservedBook();
            }
        }
        public string TitleOfLoanScreen
        {
            get => _titleOfLoanScreen; set
            {
                _titleOfLoanScreen = value;
                OnProperychanged(nameof(TitleOfLoanScreen));
            }
        }
        public IEnumerable<Book> Books => _books;
        public IEnumerable<Client> Clients => _clients;
        public Client SelectedClient
        {
            get => _seletedClient;
            set
            {
                _seletedClient = value;
                OnProperychanged(nameof(SelectedClient));
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


        private string _bookSearch;
        public string BookSearch
        {
            get => _bookSearch;
            set
            {
                _bookSearch = value;
                _booksStore.SearchBookName = value;
                OnProperychanged(nameof(BookSearch));
            }
        }

        private int _booksSortOrder;
        public int BooksSortOrder
        {
            get => _booksSortOrder;
            set
            {
                _booksSortOrder = value;
                _booksStore.SearchSubject = value;
                OnProperychanged(nameof(_booksSortOrder));
            }
        }

        private string _clientSearch;
        public string ClientSearch
        {
            get => _clientSearch;
            set
            {
                _clientSearch = value;
                _clientsStore.SearchClientName = value;
                OnProperychanged(nameof(ClientSearch));
            }
        }

        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                OnProperychanged(nameof(ReturnDate));
            }
        }
        #endregion


        #region Commands

        public IViewModelBase CurrentModelViewModel => _modalNavigationStore.CurrentViewModel;
        public ICommand LoadBooksCommand { get; }
        public ICommand LoadClientsCommand { get; }
        public ICommand CloseModalCommand { get; }
        public ICommand SaveLoanDataCommand { get; }
        public ICommand SearchBookNameCommand { get; }
        public ICommand OrderBooksBySubjectCommand { get; }
        public ICommand SearchClientNameCommand { get; }
        #endregion

        #region Contructor
        public AddEditeLoanViewModel(ModalNavigationStore modalNavigationStore, ClientsStore clientsStore, BooksStore booksStore, LoansStore loanStore, LoanRepository loanRepository, SettingsStore settingsStore, BooksRepository booksRepository, ReservedBooksRepository reservedBooksRepository, MessageBoxStore messageBoxStore, Loan loan = null)
        {
            _messageBoxStore = messageBoxStore;
            _clients = new ObservableCollection<Client>();
            _books = new ObservableCollection<Book>();
            _clientsStore = clientsStore;
            _clientsStore.ClientsUpdated += OnClientsUpdated;
            _booksStore = booksStore;
            _booksStore.BooksUpdated += OnBooksUpdated;
            _loansStore = loanStore;
            _modalNavigationStore = modalNavigationStore;
            _loanRepository = loanRepository;
            _settingsStore = settingsStore;
            _booksRepository = booksRepository;
            _reservedBooksRepository = reservedBooksRepository;
            LoadBooksCommand = new LoadBooksCommand(booksStore);
            LoadClientsCommand = new LoadClientsCommand(clientsStore);
            if (loan is null)
            {
                TitleOfLoanScreen = "امانت جدید";
                ReturnDate = DateTime.Now;
            }
            else
            {
                TitleOfLoanScreen = "ویرایش امانت";
                SelectedLoan = loan;
                ReturnDate = loan.ReturnDate;
            }
            SaveLoanDataCommand = new SaveLoanDataCommand(this, _loansStore, _modalNavigationStore, _loanRepository, _settingsStore, _booksRepository, _reservedBooksRepository, _messageBoxStore);
            CloseModalCommand = new CloseModalCommand(_modalNavigationStore);
            SearchBookNameCommand = new SearchBookNameCommand(_booksStore);
            OrderBooksBySubjectCommand = new OrderBooksBySubjectCommand(_booksStore, _messageBoxStore);
            SearchClientNameCommand = new SearchClientNameCommand(_clientsStore);
            _modalNavigationStore.CurrentViewModelChanged += ModalViewModelChange;

        }

        #endregion

        #region Methods
        /// <summary>
        /// called after a book been updated
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
        /// update each time store clients list get changed
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
        /// fill data of selected loans to Book and Client
        /// </summary>
        private void SetDataOfSelectedReservedBook()
        {
            if (!(_selectedLoan is null))
            {

                Book book = _books.SingleOrDefault(b => b.ID == _selectedLoan.BookId);
                SelectedBook = book;
                Client client = _clients.SingleOrDefault(c => c.ID == _selectedLoan.ClientId);
                SelectedClient = client;
            }
        }

        /// <summary>
        /// show modal of add edit loan
        /// </summary>
        private void ModalViewModelChange()
        {
            OnProperychanged(nameof(CurrentModelViewModel));
        }
        #endregion
    }
}
