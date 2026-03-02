using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateLayoutSettingCommand : CommandBase
    {
        #region Dependencies
        private ISettingNavigationStore _settingNavigationStore;
        private IMainLayoutSettingViewModel _mainLayoutSettingViewModel = new MainLayoutSettingViewModel();
        #endregion

        #region Contructor
        /// <summary>
        /// set setting view of setting navigation to main setting navigation
        /// </summary>
        /// <param name="settingNavigationStore"></param>
        public NavigateLayoutSettingCommand(ISettingNavigationStore settingNavigationStore)
        {
            _settingNavigationStore = settingNavigationStore;

        }
        #endregion

        #region Execution
        public override void Execute(object parameter)
        {
            _settingNavigationStore.CurrentSettingViewModel = _mainLayoutSettingViewModel;
        }
        #endregion
    }
}
