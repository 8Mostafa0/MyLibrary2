using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public class ReservedBooksViewModel : ViewModelBase, IReservedBooksViewModel
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private IMessageBoxStore _messageBoxStore;
        private IReservedBooksStore _reservedBooksStore;
        private IModalNavigationStore _modalNavigationStore;
        private ObservableCollection<ReservedBookViewModel> _reservedBooks;
        private ReservedBookViewModel _selectedReservBook;
        public IEnumerable<ReservedBookViewModel> ReservedBooks => _reservedBooks;

        private string _bookName;
        public ReservedBookViewModel SelectedReservedBook
        {
            get => _selectedReservBook;
            set
            {
                if (value is null)
                {
                    _selectedReservBook = new ReservedBookViewModel(new ReservedBook(), _clientsStore, _booksStore);
                }
                else
                {
                    _selectedReservBook = value;
                }

                _reservedBooksStore.SelectedReserv = _selectedReservBook.ToReservedBook();
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
        public IRemoveReservBookCommand RemoveReservBookCommand { get; }

        public IEditeReservBookCommand EditeReservBookCommand { get; }
        public IResetReservBookCommand ResetReservBookCommand { get; }

        public ILoadReservedBooksCommand LoadReservedBooksCommand { get; }

        public ISearchBookNameInReservedBookCommand SearchBookNameInReservedBookCommand { get; }
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="resetReservBookCommand"></param>
        /// <param name="editeReservBookCommand"></param>
        /// <param name="addNewReservBookCommand"></param>
        /// <param name="removeReservBookCommand"></param>
        /// <param name="loadReservedBooksCommand"></param>
        /// <param name="searchBookNameInReservedBookCommand"></param>
        public ReservedBooksViewModel(
            IBooksStore booksStore,
            IClientsStore clientsStore,
            IMessageBoxStore messageBoxStore,
            IReservedBooksStore reservedBooksStore,
            IModalNavigationStore modalNavigationStore,
            IResetReservBookCommand resetReservBookCommand,
            IEditeReservBookCommand editeReservBookCommand,
            IAddNewReservBookCommand addNewReservBookCommand,
            IRemoveReservBookCommand removeReservBookCommand,
            ILoadReservedBooksCommand loadReservedBooksCommand,
            ISearchBookNameInReservedBookCommand searchBookNameInReservedBookCommand
            )
        {
            _reservedBooks = new ObservableCollection<ReservedBookViewModel>();

            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _messageBoxStore = messageBoxStore;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            ResetReservBookCommand = resetReservBookCommand;
            EditeReservBookCommand = editeReservBookCommand;
            AddNewReservBookCommand = addNewReservBookCommand;
            RemoveReservBookCommand = removeReservBookCommand;
            LoadReservedBooksCommand = loadReservedBooksCommand;
            SearchBookNameInReservedBookCommand = searchBookNameInReservedBookCommand;


            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;
            _reservedBooksStore.ReseredBooksUpdated += UpdateReservedBooks;
            _reservedBooksStore.ReservBookEdited += OnReservedBookUpdate;
            _reservedBooksStore.ReservBookAdded += OnReservedBookAdded;
            _reservedBooksStore.ReservBookDeleted += OnReservedBookDeleted;
            LoadReservedBooksCommand.Execute(null);
        }
        #endregion

        #region Methods
        /// <summary>
        /// get called each time a reserved book event get trigred to change its value in reservedbooks list
        /// </summary>
        /// <param name="book"></param>
        private void OnReservedBookUpdate(ReservedBook book)
        {
            ReservedBookViewModel reserveBook = _reservedBooks.SingleOrDefault(t => t.ID == book.ID);
            int index = _reservedBooks.IndexOf(reserveBook);
            if (index > 0)
            {
                _reservedBooks[index] = new ReservedBookViewModel(book, _clientsStore, _booksStore);
                _messageBoxStore.Show("رزرو با موفقیت ویرایش شد", "ویرایش رزرو");
            }
        }
        /// <summary>
        /// get call each time a reserved book event triger to remove it from reserved books list
        /// </summary>
        /// <param name="book"></param>
        private void OnReservedBookDeleted(ReservedBook book)
        {
            SelectedReservedBook = null;
            int index = _reservedBooks.IndexOf(_reservedBooks.SingleOrDefault(t => t.BookId == book.BookId));
            if (index > 0)
            {
                ReservedBookViewModel reserveBook = _reservedBooks.SingleOrDefault(t => t.BookId == book.BookId);
                _reservedBooks.Remove(reserveBook);
                _messageBoxStore.Show("رزرو کتاب با موفقیت حذف شد", "حذف رزرو");
            }
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
            SelectedReservedBook = new ReservedBookViewModel(new ReservedBook(), _clientsStore, _booksStore);
            _reservedBooks.Clear();
            foreach (ReservedBook reservedBooks in _reservedBooksStore.ReservedBook)
                _reservedBooks.Add(new ReservedBookViewModel(reservedBooks, _clientsStore, _booksStore));
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
