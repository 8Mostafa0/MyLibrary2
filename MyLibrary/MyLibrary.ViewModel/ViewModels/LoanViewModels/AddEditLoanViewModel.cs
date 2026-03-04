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

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public class AddEditeLoanViewModel : ViewModelBase, IAddEditeLoanViewModel
    {

        #region Dependencies
        private Loan _selectedLoan;
        private IClientsStore _clientsStore;
        private IBooksStore _booksStore;
        private ObservableCollection<Book> _books;
        private ObservableCollection<Client> _clients;
        private Client _seletedClient;
        private Book _selectedBook;
        private ILoansStore _loansStore;
        private ILoanRepository _loanRepository;
        private IModalNavigationStore _modalNavigationStore;
        private ISettingsStore _settingsStore;
        private IBooksRepository _booksRepository;
        private IReservedBooksRepository _reservedBooksRepository;
        private IMessageBoxStore _messageBoxStore;
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
                _loansStore.SelectedClient = value;
                OnProperychanged(nameof(SelectedClient));
            }
        }


        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                _loansStore.SelectedBook = value;
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
                _loansStore.SelectedLoan._loan.ReturnDate = value;
                OnProperychanged(nameof(ReturnDate));
            }
        }
        #endregion


        #region Commands

        public IViewModelBase CurrentModelViewModel => _modalNavigationStore.CurrentViewModel;
        public ILoadBooksCommand LoadBooksCommand { get; }
        public ILoadClientsCommand LoadClientsCommand { get; }
        public ICloseModalCommand CloseModalCommand { get; }
        public ISaveLoanDataCommand SaveLoanDataCommand { get; }
        public ISearchBookNameCommand SearchBookNameCommand { get; }
        public IOrderBooksBySubjectCommand OrderBooksBySubjectCommand { get; }
        public ISearchClientNameCommand SearchClientNameCommand { get; }
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="settingsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="booksRepository"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="loadBooksCommand"></param>
        /// <param name="closeModalCommand"></param>
        /// <param name="loadClientsCommand"></param>
        /// <param name="saveLoanDataCommand"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="searchBookNameCommand"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="searchClientNameCommand"></param>
        /// <param name="orderBooksByStateCommand"></param>
        /// <param name="orderBooksBySubjectCommand"></param>
        public AddEditeLoanViewModel(
            IBooksStore booksStore,
            ILoansStore loansStore,
            IClientsStore clientsStore,
            ISettingsStore settingsStore,
            ILoanRepository loanRepository,
            IBooksRepository booksRepository,
            IMessageBoxStore messageBoxStore,
            ILoadBooksCommand loadBooksCommand,
            ICloseModalCommand closeModalCommand,
            ILoadClientsCommand loadClientsCommand,
            ISaveLoanDataCommand saveLoanDataCommand,
            IModalNavigationStore modalNavigationStore,
            ISearchBookNameCommand searchBookNameCommand,
            IReservedBooksRepository reservedBooksRepository,
            ISearchClientNameCommand searchClientNameCommand,
            IOrderBooksByStateCommand orderBooksByStateCommand,
            IOrderBooksBySubjectCommand orderBooksBySubjectCommand
            )
        {
            _books = new ObservableCollection<Book>();
            _clients = new ObservableCollection<Client>();

            _booksStore = booksStore;
            _loansStore = loansStore;
            _clientsStore = clientsStore;
            _settingsStore = settingsStore;
            _loanRepository = loanRepository;
            _booksRepository = booksRepository;
            _messageBoxStore = messageBoxStore;
            LoadBooksCommand = loadBooksCommand;
            CloseModalCommand = closeModalCommand;
            LoadClientsCommand = loadClientsCommand;
            SaveLoanDataCommand = saveLoanDataCommand;
            _modalNavigationStore = modalNavigationStore;
            SearchBookNameCommand = searchBookNameCommand;
            SearchClientNameCommand = searchClientNameCommand;
            _reservedBooksRepository = reservedBooksRepository;
            OrderBooksBySubjectCommand = orderBooksBySubjectCommand;

            LoadBooksCommand.Execute(null);
            LoadClientsCommand.Execute(null);

            _booksStore.BooksUpdated += OnBooksUpdated;
            _clientsStore.ClientsUpdated += OnClientsUpdated;
            _modalNavigationStore.CurrentViewModelChanged += ModalViewModelChange;

            OnClientsUpdated();
            OnBooksUpdated();
            if (_loansStore.SelectedLoan.ID == 0)
            {
                TitleOfLoanScreen = "امانت جدید";
                ReturnDate = DateTime.Now;
            }
            else
            {
                TitleOfLoanScreen = "ویرایش امانت";
                SelectedLoan = _loansStore.SelectedLoan._loan;
                ReturnDate = _loansStore.SelectedLoan.ReturnDate;
            }

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
                _messageBoxStore.Show(_books.Count().ToString(), "here");
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
