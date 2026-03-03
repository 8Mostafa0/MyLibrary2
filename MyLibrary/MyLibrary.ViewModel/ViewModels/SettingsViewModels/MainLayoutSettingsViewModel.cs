using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class MainLayoutSettingViewModel : ViewModelBase, IMainLayoutSettingViewModel
    {
        #region Dependencies
        private ISettingsStore _settingStore;
        private bool _booksCount;
        public bool BooksCount
        {
            get => _booksCount;
            set
            {
                _booksCount = value;
                OnProperychanged(nameof(BooksCount));
                OnDataChanged();
            }
        }
        private bool _loansCount;
        public bool LoansCount
        {
            get => _loansCount;
            set
            {
                _loansCount = value;
                OnProperychanged(nameof(LoansCount));
                OnDataChanged();
            }
        }
        private bool _dilayedLoansCount;
        public bool DilayedLoansCount
        {
            get => _dilayedLoansCount;
            set
            {
                _dilayedLoansCount = value;
                OnProperychanged(nameof(DilayedLoansCount));
                OnDataChanged();
            }
        }
        private bool _clientsCount;
        public bool ClientsCount
        {
            get => _clientsCount;
            set
            {
                _clientsCount = value;
                OnProperychanged(nameof(ClientsCount));
                OnDataChanged();
            }
        }
        #endregion

        #region Contructor
        public MainLayoutSettingViewModel()
        {
            _settingStore = ClassFactory.CreateSettingsStore();
            Dictionary<string, bool> settings = _settingStore.GetLayoutSettings();
            ClientsCount = settings["ShowClientsCount"];
            BooksCount = settings["ShowBooksCount"];
            LoansCount = settings["ShowLoanedBooksCount"];
            DilayedLoansCount = settings["ShowNotReturnedLoans"];
        }

        #endregion

        #region Methods


        private void OnDataChanged()
        {
            Dictionary<string, bool> settings = _settingStore.GetLayoutSettings();
            settings["ShowClientsCount"] = ClientsCount;
            settings["ShowBooksCount"] = BooksCount;
            settings["ShowLoanedBooksCount"] = LoansCount;
            settings["ShowNotReturnedLoans"] = DilayedLoansCount;
            _settingStore.SaveLayoutSettings(settings);
        }
        #endregion
    }
}
