using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public class AddEditeLoanViewModel : PropertyChangedBase, IAddEditeLoanViewModel
    {

        #region Dependencies
        private Book _selectedBook;
        private Loan _selectedLoan;
        private Client _seletedClient;
        private IMyLibraryDbContext _db;
        private IMessageBoxStore _messageBoxStore;
        private IApplicationStore _applicationStore;
        private IModalNavigationStore _modalNavigationStore;
        private ObservableCollection<Book> _books;
        private ObservableCollection<Client> _clients;
        private string _titleOfLoanScreen;

        public Loan SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                SetField(ref _selectedLoan, value);
                SetDataOfSelectedLoan();
                OnLoanDataChanged();
            }
        }
        public string TitleOfLoanScreen
        {
            get => _titleOfLoanScreen; set
            {
                SetField(ref _titleOfLoanScreen, value);
            }
        }
        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                SetField(ref _books, value);
            }
        }
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                SetField(ref _clients, value);
            }
        }
        public Client SelectedClient
        {
            get => _seletedClient;
            set
            {
                SetField(ref _seletedClient, value);
                OnLoanDataChanged();
            }
        }


        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                SetField(ref _selectedBook, value);
                OnLoanDataChanged();
            }
        }


        private string _bookSearch;
        public string BookSearch
        {
            get => _bookSearch;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    LoadBooks();
                }
                SetField(ref _bookSearch, value);
                SearchBookNameCommand.RaiseCanExecuteChanged();
            }
        }

        private int _booksSortOrder;
        public int BooksSortOrder
        {
            get => _booksSortOrder;
            set
            {
                SetField(ref _booksSortOrder, value);
            }
        }

        private string _clientSearch;
        public string ClientSearch
        {
            get => _clientSearch;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    LoadClients();
                }
                SetField(ref _clientSearch, value);
                SearchClientNameCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _returnDate;
        public DateTime ReturnDate
        {
            get => _returnDate;
            set
            {
                SetField(ref _returnDate, DateTime.Parse(value.ToString("O")));
                OnLoanDataChanged();
            }
        }
        #endregion


        #region Commands

        public IViewModelBase CurrentModelViewModel => _modalNavigationStore.CurrentViewModel;
        public ILoadBooksCommand LoadBooksCommand { get; }
        public RelayCommand CloseModalCommand { get; }
        public AsyncRelayCommand SaveLoanDataCommand { get; }
        public AsyncRelayCommand SearchBookNameCommand { get; }
        public AsyncRelayCommand OrderBooksBySubjectCommand { get; }
        public AsyncRelayCommand SearchClientNameCommand { get; }
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="applicationStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="modalNavigationStore"></param>
        public AddEditeLoanViewModel(
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

            _books = new ObservableCollection<Book>();
            _clients = new ObservableCollection<Client>();
            CloseModalCommand = new RelayCommand(CloseModal);
            SaveLoanDataCommand = new AsyncRelayCommand(SaveNewLoan, ValidateLoanData);
            SearchBookNameCommand = new AsyncRelayCommand(SearchBooksName, () => { return BookSearch != ""; });
            SearchClientNameCommand = new AsyncRelayCommand(SearchClientName, () => { return ClientSearch != ""; });
            OrderBooksBySubjectCommand = new AsyncRelayCommand(OrderBooksBySubject);
            _modalNavigationStore.CurrentViewModelChanged += ModalViewModelChange;

            BooksSortOrder = 0;
            if (_applicationStore is null || _applicationStore.SelectedLoan is null || _applicationStore.SelectedLoan.Id == 0)
            {
                TitleOfLoanScreen = "امانت جدید";
                ReturnDate = DateTime.Now;
                SelectedLoan = Loan.Empty();
            }
            else
            {
                TitleOfLoanScreen = "ویرایش امانت";
                SelectedLoan = _applicationStore.SelectedLoan;
                ReturnDate = _applicationStore.SelectedLoan.ReturnDate;
            }
        }


        private async Task OrderBooksBySubject()
        {
            List<Book> books = await _db.BooksRepository.GetBoooksBySubject(BooksSortOrder);
            _books.Clear();
            foreach (Book book in books)
            {
                _books.Add(book);
            }
        }

        private void OnLoanDataChanged()
        {
            SaveLoanDataCommand.RaiseCanExecuteChanged();
        }

        private bool ValidateLoanData()
        {
            if (SelectedBook is null) return false;
            if (SelectedClient is null) return false;
            if (ReturnDate <= DateTime.Now) return false;
            return true;
        }
        private async Task SaveNewLoan()
        {
            Loan newLoan = new Loan()
            {
                Id = SelectedLoan.Id,
                ClientId = SelectedClient.ID,
                ClientName = SelectedClient.FirstName + " " + SelectedClient.LastName,
                BookId = SelectedBook.ID,
                BookName = SelectedBook.Name,
                ReturnDate = ReturnDate
            };
            int result = 0;
            if (SelectedLoan.Id == Loan.Empty().Id)
            {
                result = await _db.LoanRepository.AddNewLoanToDb(newLoan);
            }
            else
            {
                result = await _db.LoanRepository.UpdateLoanAtDb(newLoan);
            }

            if (result > 0)
            {
                _messageBoxStore.Show($"{TitleOfLoanScreen} با موفقیت انجام شد", TitleOfLoanScreen);
                CloseModal();
            }
            else
            {
                _messageBoxStore.Show($"هنگام {TitleOfLoanScreen} مشکلی بوجود امده است", TitleOfLoanScreen);
            }
        }

        public static async Task<IAddEditeLoanViewModel> InitlizeViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore)
        {
            AddEditeLoanViewModel viewModel = new AddEditeLoanViewModel(db, applicationStore, messageBoxStore, modalNavigationStore);
            await viewModel.RefreshPage();
            return viewModel;

        }

        #endregion

        #region Methods

        private async Task SearchClientName()
        {
            List<Client> clients = await _db.ClientsRepository.GetClientsByName(ClientSearch);
            _clients.Clear();
            foreach (Client client in clients)
            {
                _clients.Add(client);
            }
        }

        private async Task SearchBooksName()
        {
            List<Book> books = await _db.BooksRepository.GetBooksByName(BookSearch);
            _books.Clear();
            foreach (Book book in books)
            {
                _books.Add(book);
            }
        }

        private void CloseModal()
        {
            _modalNavigationStore?.Close();
        }
        private async Task LoadBooks()
        {
            List<Book> books = await _db.BooksRepository.GetAllBooks();
            _books.Clear();
            foreach (Book book in books)
            {
                _books.Add(book);
            }

        }

        private async Task LoadClients()
        {
            List<Client> clients = await _db.ClientsRepository.GetAllClients();
            Clients.Clear();
            foreach (Client client in clients)
            {
                Clients.Add(client);
            }
        }

        private void ClearInputs()
        {
            BookSearch = "";
            ClientSearch = "";
        }

        public async Task RefreshPage()
        {
            await LoadBooks();
            await LoadClients();
            ClearInputs();
        }


        /// <summary>
        /// fill data of selected loans to Book and Client
        /// </summary>
        private void SetDataOfSelectedLoan()
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
            OnPropertyChanged(nameof(CurrentModelViewModel));
        }

        #endregion
    }
}
