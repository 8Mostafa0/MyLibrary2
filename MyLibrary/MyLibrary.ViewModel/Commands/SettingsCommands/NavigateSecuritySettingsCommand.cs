using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateSecuritySettingsCommand : CommandBase, INavigateSecuritySettingsCommand
    {
        #region Dependencies
        private ISettingNavigationStore _settingNavigationStore;
        private ISecuritySettingsViewModel _securitySettingsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// set current view of setting navigation to security (change password view) view
        /// </summary>
        /// <param name="settingNavigationStore"></param>
        /// <param name="securitySettingsViewModel"></param>
        public NavigateSecuritySettingsCommand(ISettingNavigationStore settingNavigationStore, ISecuritySettingsViewModel securitySettingsViewModel)
        {
            _settingNavigationStore = settingNavigationStore;
            _securitySettingsViewModel = securitySettingsViewModel;
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
