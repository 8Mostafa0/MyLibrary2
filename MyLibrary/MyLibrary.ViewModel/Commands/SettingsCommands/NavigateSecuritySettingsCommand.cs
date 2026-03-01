using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateSecuritySettingsCommand : CommandBase
    {
        #region Dependencies
        private SettingNavigationStore _settingNavigationStore;
        private SecuritySettingsViewModel _securitySettingsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of setting navigation to security (change password view) view
        /// </summary>
        /// <param name="settingNavigationStore"></param>
        public NavigateSecuritySettingsCommand(SettingNavigationStore settingNavigationStore)
        {
            _settingNavigationStore = settingNavigationStore;
            _securitySettingsViewModel = new SecuritySettingsViewModel(_settingNavigationStore);
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _settingNavigationStore.CurrentSettingViewModel = _securitySettingsViewModel;
        }
        #endregion
    }
}
