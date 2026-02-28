using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class LoanSettingsViewModel : ViewModelBase
    {
        #region Dependencies
        private SettingsStore _settingsStore;
        private int _maxBooksCount;
        public string MaxBooksCount
        {
            get => _maxBooksCount.ToString();

            set
            {
                if (!int.TryParse(value, out _maxBooksCount))
                {
                    MessageBox.Show("لطفا تعداد حداکثر کتاب را عدد وارد کنید", "خطای ورودی");
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
                    MessageBox.Show("لطفا تعداد حداکثر تعداد روز را عدد وارد کنید", "خطای ورودی");
                    return;
                }
                OnProperychanged(nameof(MaxLoanDay));
                SettingsChanged();
            }
        }
        #endregion

        #region Constructor
        public LoanSettingsViewModel()
        {
            _settingsStore = new SettingsStore();
            Dictionary<string, int> setting = _settingsStore.GetLoansSetting();
            MaxBooksCount = setting["MaxBooksLoan"].ToString();
            MaxLoanDay = setting["MaxLoanDays"].ToString();
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
