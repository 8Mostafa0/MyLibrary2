using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Stores;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Dependencies
        private SettingNavigationStore _settingNavigationStore;
        private MessageBoxStore _messageBoxStore;

        public ViewModelBase CurrentSettingViewModel => _settingNavigationStore.CurrentSettingViewModel;

        #endregion

        #region Commands
        public ICommand NavigateLayoutSettingCommand { get; }
        public ICommand NavigateLoanSettingsCommand { get; }
        public ICommand NavigateSecuritySettingsCommand { get; }
        #endregion

        #region Contructor
        public SettingsViewModel(SettingNavigationStore settingNavigationStore, MessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
            _settingNavigationStore = settingNavigationStore;
            _settingNavigationStore.SettingViewModelChanged += OnSettingViewModelChanged;
            NavigateLayoutSettingCommand = new NavigateLayoutSettingCommand(_settingNavigationStore);
            NavigateLoanSettingsCommand = new NavigateLoanSettingsCommand(_settingNavigationStore, _messageBoxStore);
            NavigateSecuritySettingsCommand = new NavigateSecuritySettingsCommand(_settingNavigationStore, _messageBoxStore);
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
