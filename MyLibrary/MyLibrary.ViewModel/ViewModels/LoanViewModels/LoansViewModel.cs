using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public class LoansViewModel : ViewModelBase, ILoansViewModel
    {
        #region Dependencies
        private IModalNavigationStore _modalNavigationStore;
        private ObservableCollection<LoanViewModel> _loans;
        private ILoansStore _loansStore;
        private IClientsStore _clientsStore;
        private IBooksStore _booksStore;
        private LoanRepository _loanRepository;
        private SettingsStore _settingsStore;
        private BooksRepository _booksRepository;
        private ReservedBooksRepository _reservedBooksRepository;
        private IMessageBoxStore _messageBoxStore;
        public IViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public IEnumerable<LoanViewModel> Loans => _loans;

        private int _sortIndex;
        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                _sortIndex = value;
            }
        }
        private string _bookName;
        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;
                OnProperychanged(nameof(BookName));
            }
        }
        private LoanViewModel _selectedLoan;
        public LoanViewModel SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                _selectedLoan = value;
            }
        }
        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;

        #endregion

        #region Commands

        public ICommand ShowAddLoanModalCommand { get; }
        public ICommand ShowEditLoanViewModel { get; }
        public ICommand LoadLoansCommand { get; }
        public ICommand OrderBooksCommand { get; }
        public ICommand SearchBookCommand { get; }
        public ICommand ReturnedLoanCommand { get; }
        public ICommand ReloadLoansListCommand { get; }
        public ICommand SortLoansListCommand { get; }
        #endregion

        #region Constructor
        public LoansViewModel(IModalNavigationStore modalNavigationStore, ILoansStore loansStore, IClientsStore clientsStore, IBooksStore booksStore, LoanRepository loanRepository, SettingsStore settingsStore, BooksRepository booksRepository, ReservedBooksRepository reservedBooksRepository, IMessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;
            _loans = new ObservableCollection<LoanViewModel>();
            _modalNavigationStore = modalNavigationStore;
            _loansStore = loansStore;
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            _loanRepository = loanRepository;
            _settingsStore = settingsStore;
            _booksRepository = booksRepository;
            _reservedBooksRepository = reservedBooksRepository;
            LoadLoansCommand = new LoadLoansCommand(_loansStore);
            ShowAddLoanModalCommand = new ShowLoanModalCommand(_modalNavigationStore, _booksStore, _clientsStore, _loansStore, _loanRepository, _settingsStore, _booksRepository, _reservedBooksRepository, _messageBoxStore);
            ShowEditLoanViewModel = new ShowEditLoanViewModel(_modalNavigationStore, _loansStore, _booksStore, _clientsStore, this, _loanRepository, _settingsStore, _booksRepository, _messageBoxStore, _reservedBooksRepository);
            SortLoansListCommand = new SortLoansListCommand(this, _loansStore);
            ReturnedLoanCommand = new ReturnedLoanCommand(this, _loansStore, _messageBoxStore);
            ReloadLoansListCommand = new ReloadLoansListCommand(_loansStore);
            SearchBookCommand = new SearchBookCommand(this, _loansStore);
            _loansStore.LoansUpdated += UpdateLoans;
            _loansStore.LoanIsAdded += LoanAdded;
            _loansStore.LoanIsUpdated += LoanIsUpdated;
            _loansStore.LoanIsReturned += LoanIsReturned;
            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;
            SelectedLoan = LoanViewModel.Empty();
            _messageBoxStore = messageBoxStore;
        }

        private void LoanIsReturned(Loan loan)
        {
            LoanViewModel loanViewModel = _loans.SingleOrDefault(t => t._loan.Id == loan.Id);
            int index = _loans.IndexOf(loanViewModel);
            _loans[index] = new LoanViewModel(loan, _clientsStore, _booksStore);
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
            var updatedVm = new LoanViewModel(loan, _clientsStore, _booksStore);

            var existing = _loans.FirstOrDefault(l => l.ID == loan.Id);
            if (existing != null)
            {
                int index = _loans.IndexOf(existing);
                _loans[index] = updatedVm;
                _messageBoxStore.Show("امانت با موفقیت ویرایش شد", "ویرایش امانت");
            }
            OnProperychanged(nameof(_loans));
        }

        /// <summary>
        /// cal each time new loan add and add it to the loans list
        /// </summary>
        /// <param name="loan"></param>
        private void LoanAdded(Loan loan)
        {
            loan.Id = _loans.Any() ? _loans.Last().ID + 1 : 1;
            var vm = new LoanViewModel(loan, _clientsStore, _booksStore);
            _loans.Add(vm);
            _messageBoxStore.Show("امانت با موفقیت ثبت شد", "افزودن امانت");
        }

        /// <summary>
        /// called when modal navigation store view set to a view or get null
        /// </summary>
        private void OnModalViewModelChanged()
        {
            OnProperychanged(nameof(CurrentModalViewModel));
            OnProperychanged(nameof(IsModalOpen));
        }
        /// <summary>
        /// called each time loans list get updated
        /// </summary>
        public void UpdateLoans()
        {
            _loans.Clear();
            foreach (LoanViewModel loan in _loansStore.Loans)
            {
                _loans.Add(loan);
            }
        }


        #endregion
    }
}
