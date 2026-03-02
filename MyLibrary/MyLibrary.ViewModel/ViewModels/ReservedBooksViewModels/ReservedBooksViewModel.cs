using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public class ReservedBooksViewModel : ViewModelBase
    {
        #region Dependencies
        private ModalNavigationStore _modalNavigationStore;
        private BooksStore _booksStore;
        private ClientsStore _clientsStore;
        private ReservedBooksStore _reservedBooksStore;
        private string _bookName;
        private ObservableCollection<ReservedBookViewModel> _reservedBooks;
        private MessageBoxStore _messageBoxStore;
        public IEnumerable<ReservedBookViewModel> ReservedBooks => _reservedBooks;
        private ReservedBookViewModel _selectedReservBook;

        public ReservedBookViewModel SelectedReservedBook
        {
            get => _selectedReservBook;
            set
            {
                _selectedReservBook = value;
                OnProperychanged(nameof(SelectedReservedBook));
            }
        }

        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;
                _booksStore.SearchBookName = value;
                OnProperychanged(nameof(_bookName));
            }
        }

        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        #endregion

        #region Commands

        public ICommand AddNewReservBookCommand { get; }
        public ICommand RemoveReservBookCommand { get; }

        public ICommand EditeReservBookCommand { get; }
        public ICommand ResetReservBookCommand { get; }

        public ICommand LoadReservedBooksCommand { get; }

        public ICommand SearchBookNameInReservedBookCommand { get; }
        #endregion

        #region Contructor

        public ReservedBooksViewModel(ReservedBooksStore reservedBooksStore, ModalNavigationStore modalNavigationStore, ClientsStore clientsStore, BooksStore booksStore, LoanRepository loansRepository, ClientsRepository clientsRepository, ReservedBooksRepository reservedBooksRepository, MessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
            _reservedBooksStore = reservedBooksStore;
            _reservedBooks = new ObservableCollection<ReservedBookViewModel>();
            _modalNavigationStore = modalNavigationStore;
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;
            _reservedBooksStore.ReseredBooksUpdated += UpdateReservedBooks;
            _reservedBooksStore.ReservBookEdited += OnReservedBookUpdate;
            _reservedBooksStore.ReservBookAdded += OnReservedBookAdded;
            _reservedBooksStore.ReservBookDeleted += OnReservedBookDeleted;
            LoadReservedBooksCommand = new LoadReservedBooksCommand(_reservedBooksStore);
            EditeReservBookCommand = new EditeReservBookCommand(
                _booksStore,
                _clientsStore,
                loansRepository,
                clientsRepository,
                _reservedBooksStore,
                _modalNavigationStore,
                this,
                reservedBooksRepository,
                _messageBoxStore
                );
            AddNewReservBookCommand = new AddNewReservBookCommand(
                _modalNavigationStore,
                _reservedBooksStore,
                _clientsStore,
                _booksStore,
                loansRepository,
                reservedBooksRepository,
                clientsRepository,
                _messageBoxStore
                );
            RemoveReservBookCommand = new RemoveReservBookCommand(this, _reservedBooksStore, _messageBoxStore);
            ResetReservBookCommand = new ResetReservBookCommand(_reservedBooksStore);
            SearchBookNameInReservedBookCommand = new SearchBookNameInReservedBookCommand(this, _reservedBooksStore);

        }
        #endregion

        #region Methods
        /// <summary>
        /// get called each time a reserved book event get trigred to change its value in reservedbooks list
        /// </summary>
        /// <param name="book"></param>
        private void OnReservedBookUpdate(ReservedBook book)
        {
            ReservedBookViewModel reserveBook = _reservedBooks.SingleOrDefault(t => t.BookId == book.ID);
            int index = _reservedBooks.IndexOf(reserveBook);
            _reservedBooks.RemoveAt(index);
            _reservedBooks.Add(reserveBook);
            int newIndex = _reservedBooks.IndexOf(reserveBook);
            _reservedBooks.Move(newIndex, index);
            _messageBoxStore.Show("رزرو با موفقیت ویرایش شد", "ویرایش رزرو");
        }
        /// <summary>
        /// get call each time a reserved book event triger to remove it from reserved books list
        /// </summary>
        /// <param name="book"></param>
        private void OnReservedBookDeleted(ReservedBook book)
        {
            _reservedBooks.Remove(_selectedReservBook);
            _messageBoxStore.Show("رزرو کتاب با موفقیت حذف شد", "حذف رزرو");
        }
        /// <summary>
        /// called each time reserved book add event get trigred to add it to reserved books list
        /// </summary>
        /// <param name="book"></param>

        private void OnReservedBookAdded(ReservedBook book)
        {
            book.ID = _reservedBooks.Any() ? _reservedBooks.Last().ID + 1 : 1;
            ReservedBookViewModel Reserv = new ReservedBookViewModel(book, _clientsStore, _booksStore);
            _reservedBooks.Add(Reserv);
            _messageBoxStore.Show("کتاب با موفقیت رزرو شد", "رزور کتاب");
        }

        /// <summary>
        /// get updated each time reservedbook list in reservedbooks store get changed
        /// </summary>
        private void UpdateReservedBooks()
        {
            _reservedBooks.Clear();
            foreach (ReservedBook reservedBooks in _reservedBooksStore.ReservedBook)
                _reservedBooks.Add(new ReservedBookViewModel(reservedBooks, _clientsStore, _booksStore));
            SelectedReservedBook = null;
        }

        /// <summary>
        /// called each time modal navigation view get changed
        /// </summary>
        private void OnModalViewModelChanged()
        {
            OnProperychanged(nameof(CurrentModalViewModel));
            OnProperychanged(nameof(IsModalOpen));
        }

        /// <summary>
        /// Loader method for reservedbooks view model
        /// </summary>
        /// <param name="reservedBooksStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loansRepository"></param>
        /// <param name="clientsRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <returns></returns>
        public static ReservedBooksViewModel LoadViewModel(ReservedBooksStore reservedBooksStore, ModalNavigationStore modalNavigationStore, ClientsStore clientsStore, BooksStore booksStore, LoanRepository loansRepository, ClientsRepository clientsRepository, ReservedBooksRepository reservedBooksRepository, MessageBoxStore messageBoxStore)
        {
            ReservedBooksViewModel ViewModel = new ReservedBooksViewModel(reservedBooksStore, modalNavigationStore, clientsStore, booksStore, loansRepository, clientsRepository, reservedBooksRepository, messageBoxStore);
            ViewModel.LoadReservedBooksCommand.Execute(null);
            return ViewModel;
        }

        #endregion
    }
}
