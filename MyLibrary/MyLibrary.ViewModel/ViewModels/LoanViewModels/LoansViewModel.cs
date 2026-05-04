using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public class LoansViewModel : PropertyChangedBase, ILoansViewModel
    {
        #region Dependencies
        private IMyLibraryDbContext _db;
        private IApplicationStore _applicationStore;
        private IModalNavigationStore _modalNavigationStore;
        private ObservableCollection<Loan> _loans;
        private IMessageBoxStore _messageBoxStore;
        public IViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public ObservableCollection<Loan> Loans
        {
            get => _loans;
            set
            {
                SetField(ref _loans, value);
            }
        }

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
                if (string.IsNullOrEmpty(value))
                {
                    _bookName = "";
                    RefreshPage();
                }
                SetField(ref _bookName, value);
            }
        }
        private Loan _selectedLoan;
        public Loan SelectedLoan
        {
            get => _selectedLoan;
            set
            {
                SetField(ref _selectedLoan, value ?? Loan.Empty());
                ReturnedLoanCommand?.RaiseCanExecuteChanged();
                ShowEditLoanViewModelCommand?.RaiseCanExecuteChanged();
            }
        }
        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;

        #endregion

        #region Commands

        public AsyncRelayCommand ShowAddLoanViewModalCommand { get; }
        public AsyncRelayCommand ShowEditLoanViewModelCommand { get; }
        public AsyncRelayCommand SearchBookCommand { get; }
        public AsyncRelayCommand ReturnedLoanCommand { get; }
        public AsyncRelayCommand ReloadLoansListCommand { get; }
        public AsyncRelayCommand SortLoansListCommand { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="applicationStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="modalNavigationStore"></param>
        public LoansViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore
            )
        {
            _loans = new ObservableCollection<Loan>();
            _db = db;
            _applicationStore = applicationStore;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;
            SearchBookCommand = new AsyncRelayCommand(SearchLoansByBookName);
            ReturnedLoanCommand = new AsyncRelayCommand(LoanIsReturned, () => { return SelectedLoan.Id != Loan.Empty().Id; });
            SortLoansListCommand = new AsyncRelayCommand(SortLoansByOrder);
            ShowEditLoanViewModelCommand = new AsyncRelayCommand(ShowEditeLoanViewModel, () => { return SelectedLoan.Id != Loan.Empty().Id; });
            ShowAddLoanViewModalCommand = new AsyncRelayCommand(ShowAddLoanViewModel);
            ReloadLoansListCommand = new AsyncRelayCommand(RefreshPage);


            _modalNavigationStore.CurrentViewModelChanged += OnModalViewModelChanged;


        }

        private async Task SearchLoansByBookName()
        {
            List<Loan> loans = await _db.LoanRepository.GetLoansByBookName(BookName);
            _loans.Clear();
            foreach (Loan loan in loans)
            {
                _loans.Add(loan);
            }
        }

        private async Task SortLoansByOrder()
        {
            List<Loan> loans = new List<Loan>();
            switch (SortIndex)
            {
                case 0: loans = await _db.LoanRepository.GetAllLoans(); break;
                case 1: loans = await _db.LoanRepository.GetNotReturnedLoans(); break;
                case 2: loans = await _db.LoanRepository.GetDilayedLoans(); break;
                case 3: loans = await _db.LoanRepository.GetReturnedLoans(); break;
            }
            _loans.Clear();
            foreach (Loan loan in loans)
            {
                _loans.Add(loan);
            }
        }

        public static async Task<LoansViewModel> InitializeViewModel(
            IMyLibraryDbContext db,
            IApplicationStore applicationStore,
            IMessageBoxStore messageBoxStore,
            IModalNavigationStore modalNavigationStore
            )
        {
            LoansViewModel viewModel = new LoansViewModel(db, applicationStore, messageBoxStore, modalNavigationStore);
            await viewModel.RefreshPage();
            return viewModel;
        }

        private async Task ShowEditeLoanViewModel()
        {
            _applicationStore.SelectedLoan = SelectedLoan;
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
            SortIndex = 0;
            SelectedLoan = Loan.Empty();
        }

        private async Task LoanIsReturned()
        {
            int result = await _db.LoanRepository.SetLoanReturned(SelectedLoan);
            if (result > 0)
            {
                ClearInputs();
                _messageBoxStore.Show("امانت با موفقیت بازگشت شد", "بازگشت امانت");
                await RefreshPage();
            }
            else
            {
                _messageBoxStore.Show("هنگام انجام عملیات بازگشت امانت مشکلی بوجود امده است", "بازگشت امانت");
            }
        }
        #endregion

        #region Methods


        /// <summary>
        /// called when modal navigation store view set to a view or get null
        /// </summary>
        private void OnModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
            RefreshPage();
        }
        /// <summary>
        /// called each time loans list get updated
        /// </summary>
        public async Task RefreshPage()
        {
            _loans.Clear();
            foreach (Loan loan in await _db.LoanRepository.GetAllLoans())
            {
                _loans.Add(loan);
            }
            ClearInputs();
        }


        #endregion
    }
}
