using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public class LoansViewModel : PropertyChangedBase, ILoansViewModel
    {
        #region Dependencies
        private IMyLibraryDbContext _db;
        private ApplicationStore _applicationStore;
        private IModalNavigationStore _modalNavigationStore;
        private ObservableCollection<Loan> _loans;
        private IMessageBoxStore _messageBoxStore;
        public IViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public IEnumerable<Loan> Loans => _loans;

        private int _sortIndex;
        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                SetField(ref _sortIndex, value);
            }
        }
        private string _bookName;
        public string BookName
        {
            get => _bookName;
            set
            {
                SetField(ref _bookName, value);
            }
        }
        private Loan _selectedLoan;
        public Loan SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                SetField(ref _selectedLoan, value);
            }
        }
        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;

        #endregion

        #region Commands

        public AsyncRelayCommand ShowAddLoanModalCommand { get; }
        public AsyncRelayCommand ShowEditLoanViewModel { get; }
        public ILoadLoansCommand LoadLoansCommand { get; }
        public ISearchBookCommand SearchBookCommand { get; }
        public IReturnedLoanCommand ReturnedLoanCommand { get; }
        public IReloadLoansListCommand ReloadLoansListCommand { get; }
        public ISortLoansListCommand SortLoansListCommand { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loansStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="loadLoansCommand"></param>
        /// <param name="searchBookCommand"></param>
        /// <param name="returnedLoanCommand"></param>
        /// <param name="showLoanModalCommand"></param>
        /// <param name="sortLoansListCommand"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="showEditLoanViewModel"></param>
        /// <param name="reloadLoansListCommand"></param>
        public LoansViewModel(
            IMyLibraryDbContext db,
            ILoansStore loansStore,
            IBooksStore booksStore,
            IClientsStore clientsStore,
            IMessageBoxStore messageBoxStore,
            ILoadLoansCommand loadLoansCommand,
            ISearchBookCommand searchBookCommand,
            IReturnedLoanCommand returnedLoanCommand,
            IShowLoanModalCommand showLoanModalCommand,
            ISortLoansListCommand sortLoansListCommand,
            IModalNavigationStore modalNavigationStore,
            IShowEditLoanViewModel showEditLoanViewModel,
            IReloadLoansListCommand reloadLoansListCommand
            )
        {
            _loans = new ObservableCollection<Loan>();
            _db = db;
            _messageBoxStore = messageBoxStore;
            LoadLoansCommand = loadLoansCommand;
            SearchBookCommand = searchBookCommand;
            ReturnedLoanCommand = returnedLoanCommand;
            SortLoansListCommand = sortLoansListCommand;
            _modalNavigationStore = modalNavigationStore;
            ShowEditLoanViewModel = new AsyncRelayCommand(ShowEditeLoanViewModel);
            ShowAddLoanModalCommand = new AsyncRelayCommand(ShowAddLoanViewModel);
            ReloadLoansListCommand = reloadLoansListCommand;


            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;

            LoadLoansCommand.Execute(null);

            SelectedLoan = Loan.Empty();
        }

        private async Task ShowEditeLoanViewModel()
        {
            IAddEditeLoanViewModel viewModel = await AddEditeLoanViewModel.InitlizeViewModel(_db, _applicationStore, _messageBoxStore, _modalNavigationStore);
            viewModel.SelectedLoan = SelectedLoan;
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
        private async Task ShowAddLoanViewModel()
        {
            IAddEditeLoanViewModel viewModel = await AddEditeLoanViewModel.InitlizeViewModel(_db, _applicationStore, _messageBoxStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
        private void ClearInputs()
        {
            BookName = "";
            SortIndex = 0;
            SelectedLoan = Loan.Empty();
        }

        private void LoanIsReturned(Loan loan)
        {
            Loan selectedLoan = _loans.SingleOrDefault(t => t.Id == loan.Id);
            int index = _loans.IndexOf(selectedLoan);
            _loans[index] = loan;
            ClearInputs();
            _messageBoxStore.Show("امانت با موفقیت بازگشت شد", "بازگشت امانت");
        }
        #endregion

        #region Methods

        /// <summary>
        /// calles each time all loans list of loans store get changed
        /// </summary>
        /// <param name="loan"></param>
        private void LoanIsUpdated(Loan loan)
        {

            var existing = _loans.FirstOrDefault(l => l.Id == loan.Id);
            if (existing != null)
            {
                int index = _loans.IndexOf(existing);
                _loans[index] = loan;
                ClearInputs();
                _messageBoxStore.Show("امانت با موفقیت ویرایش شد", "ویرایش امانت");
            }
            SetField(ref _loans, _loans);
        }

        /// <summary>
        /// cal each time new loan add and add it to the loans list
        /// </summary>
        /// <param name="loan"></param>
        private void LoanAdded(Loan loan)
        {
            loan.Id = _loans.Any() ? _loans.Last().Id + 1 : 1;
            _loans.Add(loan);
            ClearInputs();
            _messageBoxStore.Show("امانت با موفقیت ثبت شد", "افزودن امانت");
        }

        /// <summary>
        /// called when modal navigation store view set to a view or get null
        /// </summary>
        private void OnModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
        /// <summary>
        /// called each time loans list get updated
        /// </summary>
        public async Task UpdateLoans()
        {
            _loans.Clear();
            foreach (Loan loan in await _db.LoanRepository.GetAllLoans())
            {
                _loans.Add(loan);
            }
        }


        #endregion
    }
}
