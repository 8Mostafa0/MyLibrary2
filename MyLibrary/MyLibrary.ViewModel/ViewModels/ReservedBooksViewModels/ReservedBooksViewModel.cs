using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public class ReservedBooksViewModel : ViewModelBase, IReservedBooksViewModel
    {
        #region Dependencies
        private IModalNavigationStore _modalNavigationStore;
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private IReservedBooksStore _reservedBooksStore;
        private string _bookName;
        private ObservableCollection<ReservedBookViewModel> _reservedBooks;
        private IMessageBoxStore _messageBoxStore;
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
        public IViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        #endregion

        #region Commands

        public IAddNewReservBookCommand AddNewReservBookCommand { get; }
        public ICommand RemoveReservBookCommand { get; }

        public IEditeReservBookCommand EditeReservBookCommand { get; }
        public ICommand ResetReservBookCommand { get; }

        public ILoadReservedBooksCommand LoadReservedBooksCommand { get; }

        public ICommand SearchBookNameInReservedBookCommand { get; }
        #endregion

        #region Contructor

        public ReservedBooksViewModel()
        {
            _reservedBooks = new ObservableCollection<ReservedBookViewModel>();

            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _booksStore = ClassFactory.CreateBooksStore();

            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;
            _reservedBooksStore.ReseredBooksUpdated += UpdateReservedBooks;
            _reservedBooksStore.ReservBookEdited += OnReservedBookUpdate;
            _reservedBooksStore.ReservBookAdded += OnReservedBookAdded;
            _reservedBooksStore.ReservBookDeleted += OnReservedBookDeleted;
            LoadReservedBooksCommand = ClassFactory.CreateLoadReservedBooksCommand();
            EditeReservBookCommand = ClassFactory.CreateEditeReservBookCommand();
            AddNewReservBookCommand = ClassFactory.CreateAddNewReservBookCommand();
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


        #endregion
    }
}
