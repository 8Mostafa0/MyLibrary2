using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class LoanSettingsViewModel : ViewModelBase, ILoanSettingsViewModel
    {
        #region Dependencies
        private ISettingsStore _settingsStore;
        private int _maxBooksCount;
        private IMessageBoxStore _messageBoxStore;
        public string MaxBooksCount
        {
            get => _maxBooksCount.ToString();

            set
            {
                if (!int.TryParse(value, out _maxBooksCount))
                {
                    _messageBoxStore.Show("لطفا تعداد حداکثر کتاب را عدد وارد کنید", "خطای ورودی");
                    return;
                }
                OnProperychanged(nameof(MaxBooksCount));
                SettingsChanged();
            }
        }
        private int _maxLoanDay;
        public string MaxLoanDay
        {
            get => _maxLoanDay.ToString();

            set
            {
                if (!int.TryParse(value, out _maxLoanDay))
                {
                    _messageBoxStore.Show("لطفا تعداد حداکثر تعداد روز را عدد وارد کنید", "خطای ورودی");
                    return;
                }
                OnProperychanged(nameof(MaxLoanDay));
                SettingsChanged();
            }
        }
        #endregion

        #region Constructor
        public LoanSettingsViewModel(IMessageBoxStore messageBoxStore)
        {
            _settingsStore = new SettingsStore();
            Dictionary<string, int> setting = _settingsStore.GetLoansSetting();
            MaxBooksCount = setting["MaxBooksLoan"].ToString();
            MaxLoanDay = setting["MaxLoanDays"].ToString();
            _messageBoxStore = messageBoxStore;
        }

        #endregion

        #region Methods
        public void SettingsChanged()
        {
            Dictionary<string, int> setting = _settingsStore.GetLoansSetting();
            setting["MaxBooksLoan"] = int.Parse(MaxBooksCount);
            setting["MaxLoanDays"] = int.Parse(MaxLoanDay);
            _settingsStore.SaveLoanSettings(setting);
        }
        #endregion
    }
}
