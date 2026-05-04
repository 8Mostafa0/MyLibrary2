using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public class ReservedBooksViewModel : PropertyChangedBase, IReservedBooksViewModel
    {
        #region Dependencies
        private IMyLibraryDbContext _db;
        private IApplicationStore _applicationStore;
        private IMessageBoxStore _messageBoxStore;
        private IModalNavigationStore _modalNavigationStore;

        private ObservableCollection<ReservedBook> _reservedBooks;
        private ReservedBook _selectedReservBook;
        public IEnumerable<ReservedBook> ReservedBooks => _reservedBooks;

        private string _bookName;
        public ReservedBook SelectedReservedBook
        {
            get
            {
                if (_selectedReservBook is null)
                {
                    SetField(ref _selectedReservBook, ReservedBook.Empty());
                }
                return _selectedReservBook;
            }
            set
            {
                if (value is null)
                {
                    SetField(ref _selectedReservBook, ReservedBook.Empty());
                }
                else
                {
                    SetField(ref _selectedReservBook, value);
                }
                EditeReservBookCommand.RaiseCanExecuteChanged();
                RemoveReservBookCommand.RaiseCanExecuteChanged();
                _applicationStore.SelectedReservedBook = _selectedReservBook;
            }
        }

        public string BookName
        {
            get => _bookName;
            set
            {
                SetField(ref _bookName, value);
                SearchBookNameInReservedBookCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;
        public IViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;

        #endregion

        #region Commands

        public AsyncRelayCommand AddNewReservBookCommand { get; }
        public AsyncRelayCommand RemoveReservBookCommand { get; }

        public AsyncRelayCommand EditeReservBookCommand { get; }
        public AsyncRelayCommand ResetReservBookCommand { get; }


        public AsyncRelayCommand SearchBookNameInReservedBookCommand { get; }
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="applicationStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="modalNavigationStore"></param>
        public ReservedBooksViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore
            )
        {
            _reservedBooks = new ObservableCollection<ReservedBook>();
            _db = db;
            _applicationStore = applicationStore;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;

            EditeReservBookCommand = new AsyncRelayCommand(ShowEditeReserveBook, IsReservedBookSelected);
            AddNewReservBookCommand = new AsyncRelayCommand(ShowAddReserveBook);
            ResetReservBookCommand = new AsyncRelayCommand(LoadReservedBooks);
            SearchBookNameInReservedBookCommand = new AsyncRelayCommand(SearchBookNameInReservedBooks, () => { return !string.IsNullOrEmpty(BookName); });
            RemoveReservBookCommand = new AsyncRelayCommand(DeleteReserveBook, IsReservedBookSelected);


            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;
        }
        #endregion

        #region Methods

        private bool IsReservedBookSelected()
        {
            return SelectedReservedBook.ID != 0;
        }

        private async Task ShowAddReserveBook()
        {
            _modalNavigationStore.CurrentViewModel = await AddEditeReserveBookViewModel.InitializeViewModel(_db, _applicationStore, _messageBoxStore, _modalNavigationStore);
        }

        private async Task ShowEditeReserveBook()
        {
            AddEditeReserveBookViewModel viewModel = await AddEditeReserveBookViewModel.InitializeViewModel(_db, _applicationStore, _messageBoxStore, _modalNavigationStore);
            viewModel.SelectedReservedBook = SelectedReservedBook;
            _modalNavigationStore.CurrentViewModel = viewModel;
        }


        public async Task<ReservedBooksViewModel> InititlizeViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore)
        {
            ReservedBooksViewModel viewModel = new ReservedBooksViewModel(db, applicationStore, messageBoxStore, modalNavigationStore);
            await viewModel.LoadReservedBooks();
            return viewModel;
        }

        public async Task LoadReservedBooks()
        {
            List<ReservedBook> reservedBooks = await _db.ReservedBooksRepository.GetAllReservedBooks();
            _reservedBooks.Clear();
            foreach (ReservedBook reservedBook in reservedBooks)
            {
                _reservedBooks.Add(reservedBook);
            }
            SelectedReservedBook = ReservedBook.Empty();

        }

        private async Task SearchBookNameInReservedBooks()
        {
            List<ReservedBook> reservedBooks = await _db.ReservedBooksRepository.SearchBookNameInReservedBooks(BookName);
            _reservedBooks.Clear();
            foreach (ReservedBook reservedBook in reservedBooks)
            {
                _reservedBooks.Add(reservedBook);
            }
            SelectedReservedBook = ReservedBook.Empty();
        }

        /// <summary>
        /// get call each time a reserved book event triger to remove it from reserved books list
        /// </summary>
        private async Task DeleteReserveBook()
        {
            int result = await _db.ReservedBooksRepository.DeleteReservedBookToDb(SelectedReservedBook.BookId);
            if (result > 0)
            {
                _messageBoxStore.Show("حذف رزور با موفقیت انجام شد", "حذف رزرو");
            }
            else
            {
                _messageBoxStore.Show("هنگام حذف رزور مشکلی بوجود امده است", "حذف رزرو");
            }
        }


        /// <summary>
        /// called each time modal navigation view get changed
        /// </summary>
        private void OnModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
            LoadReservedBooks();
        }


        #endregion
    }
}
