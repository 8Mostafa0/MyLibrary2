using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public class SettingNavigationStore : ISettingNavigationStore
    {
        #region Dependencies
        public event Action SettingViewModelChanged;

        private IViewModelBase _currentSettingViewModel;




        public IViewModelBase CurrentSettingViewModel
        {
            get => _currentSettingViewModel;
            set
            {
                _currentSettingViewModel = value;
                OnSettingViewModelChanged();
            }
        }
        #endregion

        #region Methods
        public void OnSettingViewModelChanged()
        {
            SettingViewModelChanged?.Invoke();
        }
        public SettingNavigationStore() { }
        #endregion
    }
}
