using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Stores;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Dependencies
        private SettingNavigationStore _settingNavigationStore;

        public ViewModelBase CurrentSettingViewModel => _settingNavigationStore.CurrentSettingViewModel;

        #endregion

        #region Commands
        public ICommand NavigateLayoutSettingCommand { get; }
        public ICommand NavigateLoanSettingsCommand { get; }
        public ICommand NavigateSecuritySettingsCommand { get; }
        #endregion

        #region Contructor
        public SettingsViewModel(SettingNavigationStore settingNavigationStore)
        {
            _settingNavigationStore = settingNavigationStore;
            _settingNavigationStore.SettingViewModelChanged += OnSettingViewModelChanged;
            NavigateLayoutSettingCommand = new NavigateLayoutSettingCommand(_settingNavigationStore);
            NavigateLoanSettingsCommand = new NavigateLoanSettingsCommand(_settingNavigationStore);
            NavigateSecuritySettingsCommand = new NavigateSecuritySettingsCommand(_settingNavigationStore);
        }

        #endregion

        #region Methods
        private void OnSettingViewModelChanged()
        {
            OnProperychanged(nameof(CurrentSettingViewModel));
        }
        #endregion

    }
}
