using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        #region Dependencies
        private ISettingNavigationStore _settingNavigationStore;
        private IMessageBoxStore _messageBoxStore;

        public IViewModelBase CurrentSettingViewModel => _settingNavigationStore.CurrentSettingViewModel;

        #endregion

        #region Commands
        public INavigateLayoutSettingCommand NavigateLayoutSettingCommand { get; }
        public INavigateLoanSettingsCommand NavigateLoanSettingsCommand { get; }
        public INavigateSecuritySettingsCommand NavigateSecuritySettingsCommand { get; }
        #endregion

        #region Contructor
        public SettingsViewModel()
        {
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _settingNavigationStore = ClassFactory.CreateSettingNavigationStore();
            _settingNavigationStore.SettingViewModelChanged += OnSettingViewModelChanged;
            NavigateLayoutSettingCommand = ClassFactory.CreateNavigateLayoutSettingCommand();
            NavigateLoanSettingsCommand = ClassFactory.CreateNavigateLoanSettingsCommand();
            NavigateSecuritySettingsCommand = ClassFactory.CreateNavigateSecuritySettingsCommand();
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
