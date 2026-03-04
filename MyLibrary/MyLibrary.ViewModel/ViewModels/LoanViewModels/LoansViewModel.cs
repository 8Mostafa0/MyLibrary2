using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        private IMessageBoxStore _messageBoxStore;
        public IViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public IEnumerable<LoanViewModel> Loans => _loans;

        private int _sortIndex;
        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                _loansStore.SortIndex = value;
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
                _loansStore.BookName = value;
                OnProperychanged(nameof(BookName));
            }
        }
        private LoanViewModel _selectedLoan;
        public LoanViewModel SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                _loansStore.SelectedLoan = value;
                _selectedLoan = value;
            }
        }
        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;

        #endregion

        #region Commands

        public IShowLoanModalCommand ShowAddLoanModalCommand { get; }
        public IShowEditLoanViewModel ShowEditLoanViewModel { get; }
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
            _loans = new ObservableCollection<LoanViewModel>();

            _loansStore = loansStore;
            _booksStore = booksStore;
            _clientsStore = clientsStore;
            _messageBoxStore = messageBoxStore;
            LoadLoansCommand = loadLoansCommand;
            SearchBookCommand = searchBookCommand;
            ReturnedLoanCommand = returnedLoanCommand;
            SortLoansListCommand = sortLoansListCommand;
            _modalNavigationStore = modalNavigationStore;
            ShowEditLoanViewModel = showEditLoanViewModel;
            ShowAddLoanModalCommand = showLoanModalCommand;
            ReloadLoansListCommand = reloadLoansListCommand;


            _loansStore.LoansUpdated += UpdateLoans;
            _loansStore.LoanIsAdded += LoanAdded;
            _loansStore.LoanIsUpdated += LoanIsUpdated;
            _loansStore.LoanIsReturned += LoanIsReturned;
            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;
            SelectedLoan = LoanViewModel.Empty();
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
