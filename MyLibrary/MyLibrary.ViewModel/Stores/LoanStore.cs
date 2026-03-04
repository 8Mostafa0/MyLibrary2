using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public class LoansStore : ILoansStore
    {

        #region Dependencies
        private ObservableCollection<LoanViewModel> _loans;
        public IEnumerable<LoanViewModel> Loans => _loans;
        private ILoanRepository _loanRepository;
        private IClientsStore _clientsStore;
        private IBooksStore _booksStore;

        public LoanViewModel SelectedLoan { get; set; }
        public string BookName { get; set; }
        public string ClientName { get; set; }
        public Book SelectedBook { get; set; }
        public Client SelectedClient { get; set; }
        public int SortIndex { get; set; }
        public event Action LoansUpdated;
        public event Action<Loan> LoanIsAdded;
        public event Action<Loan> LoanIsReturned;
        public event Action<Loan> LoanIsUpdated;
        public Lazy<Task> _initilizeLazy;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        public LoansStore(
            IBooksStore booksStore,
            IClientsStore clientsStore,
            ILoanRepository loanRepository
            )
        {
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            SelectedLoan = new LoanViewModel(new Loan() { Id = 0 }, _clientsStore, _booksStore);
            _loans = new ObservableCollection<LoanViewModel>();
            _initilizeLazy = new Lazy<Task>(Initialize);
            _loanRepository = loanRepository;
        }
        #endregion


        #region Methods

        /// <summary>
        /// fill loans list for first time and next time use in memory values
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            await _initilizeLazy.Value;
        }

        /// <summary>
        /// call for setloanreturned method of database and invoke returned loand event
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task LoanReturned(Loan loan)
        {
            await _loanRepository.SetLoanReturned(loan);
            LoanIsReturned?.Invoke(loan);
        }

        /// <summary>
        /// call for update loan method of database and invoke for update loan event
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task LoanUpdated(Loan loan)
        {
            await _loanRepository.UpdateLoanAtDb(loan);
            LoanIsUpdated?.Invoke(loan);
        }
        /// <summary>
        /// call for add loan method of database and invoke for add new loan event
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task AddLoan(Loan loan)
        {
            await _loanRepository.AddNewLoanToDb(loan);
            LoanIsAdded?.Invoke(loan);
        }

        /// <summary>
        /// clear all lons set in memory and fill it with new loans geted from database
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns></returns>
        public async Task GetAllLoans(string customSql = "")
        {
            IEnumerable<Loan> loans = await _loanRepository.GetAllLoans(customSql);
            _loans.Clear();
            foreach (Loan loan in loans)
            {
                LoanViewModel loanViewModel = new LoanViewModel(loan, _clientsStore, _booksStore);
                _loans.Add(loanViewModel);
            }
            LoansUpdated?.Invoke();
        }

        /// <summary>
        /// get loans list from database and fill for first time
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {
            IEnumerable<Loan> loans = await _loanRepository.GetAllLoans();
            _loans.Clear();
            foreach (Loan loan in loans)
            {
                LoanViewModel loanViewModel = new LoanViewModel(loan, _clientsStore, _booksStore);
                _loans.Add(loanViewModel);
            }
            LoansUpdated?.Invoke();
        }

        /// <summary>
        /// call for loan update method of database  and invvoke loan update event
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        public async Task UpdateLoan(Loan loan)
        {
            await _loanRepository.UpdateLoanAtDb(loan);
            LoanIsUpdated?.Invoke(loan);
        }
        #endregion
    }
}
