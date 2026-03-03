using MyLibrary.ViewModel.Commands.SettingsCommands;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageBoxStore"></param>
        /// <param name="settingNavigationStore"></param>
        /// <param name="navigateLoanSettingsCommand"></param>
        /// <param name="navigateLayoutSettingCommand"></param>
        /// <param name="navigateSecuritySettingsCommand"></param>
        public SettingsViewModel(
            IMessageBoxStore messageBoxStore,
            ISettingNavigationStore settingNavigationStore,
            INavigateLoanSettingsCommand navigateLoanSettingsCommand,
            INavigateLayoutSettingCommand navigateLayoutSettingCommand,
            INavigateSecuritySettingsCommand navigateSecuritySettingsCommand
            )
        {
            _messageBoxStore = messageBoxStore;
            _settingNavigationStore = settingNavigationStore;
            NavigateLoanSettingsCommand = navigateLoanSettingsCommand;
            NavigateLayoutSettingCommand = navigateLayoutSettingCommand;
            NavigateSecuritySettingsCommand = navigateSecuritySettingsCommand;
            _settingNavigationStore.SettingViewModelChanged += OnSettingViewModelChanged;
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
