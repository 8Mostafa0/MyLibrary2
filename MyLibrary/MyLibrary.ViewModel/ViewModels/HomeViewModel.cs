using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.ViewModel.ViewModels
{
    public class HomeViewModel : ViewModelBase, IHomeViewModel
    {

        #region Dependencies
        private SettingsStore _settingsStore;
        private string _clientsCount;
        private string _booksCount;

        private string _loansCount;
        private string _dilayedLoansCount;

        public string ShowClientsCount { get; set; }
        public string ShowDilayedLoanCount { get; set; }
        public string ShowBooksCount { get; }
        public string ShowLoansCount { get; set; }

        public string ClientsCount
        {
            get => _clientsCount;

            set
            {

                _clientsCount = value;
                OnProperychanged(nameof(_clientsCount));
            }

        }

        public string BooksCount
        {
            get => _booksCount;
            set
            {
                _booksCount = value;
                OnProperychanged(nameof(BooksCount));
            }
        }

        public string LoansCount
        {
            get => _loansCount;
            set
            {
                _loansCount = value;
                OnProperychanged(nameof(LoansCount));
            }
        }

        public string DilayedLoanCount
        {
            get => _dilayedLoansCount;
            set
            {
                _dilayedLoansCount = value;
                OnProperychanged(nameof(DilayedLoanCount));
            }
        }


        private readonly ClientsStore _clientsStore;
        private readonly IBooksStore _booksStore;
        private readonly LoansStore _loansStore;
        #endregion

        #region Cntructor
        public HomeViewModel(ClientsStore clientsStore, IBooksStore booksStore, LoansStore loansStore)
        {
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            _loansStore = loansStore;

            _settingsStore = new SettingsStore();
            Dictionary<string, bool> settings = _settingsStore.GetLayoutSettings();
            ShowClientsCount = settings["ShowClientsCount"] ? "Visible" : "Hidden";
            ShowBooksCount = settings["ShowBooksCount"] ? "Visible" : "Hidden";
            ShowLoansCount = settings["ShowLoanedBooksCount"] ? "Visible" : "Hidden";
            ShowDilayedLoanCount = settings["ShowNotReturnedLoans"] ? "Visible" : "Hidden";

            OnDataUpdated();
        }
        #endregion

        #region Methods
        /// <summary>
        /// get count of clients,books,loaned books,dilayed loans
        /// </summary>
        private void OnDataUpdated()
        {
            ClientsCount = _clientsStore.Clients.Count().ToString();
            BooksCount = _booksStore.Books.Count().ToString();
            LoansCount = _loansStore.Loans.Count().ToString();
            List<LoanViewModel> dilayedLoans = _loansStore.Loans.Where(c => c.ReturnedDateTime is null || c.ReturnedDateTime == "").Where(c => c.ReturnDate < DateTime.Now).ToList();
            DilayedLoanCount = dilayedLoans.Any() ? dilayedLoans.Count().ToString() : "0";
        }
        #endregion
    }
}
