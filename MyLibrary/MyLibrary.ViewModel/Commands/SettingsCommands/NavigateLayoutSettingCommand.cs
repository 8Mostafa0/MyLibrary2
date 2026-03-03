using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateLayoutSettingCommand : CommandBase, INavigateLayoutSettingCommand
    {
        #region Dependencies
        private ISettingNavigationStore _settingNavigationStore;
        private IMainLayoutSettingViewModel _mainLayoutSettingViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// set setting view of setting navigation to main setting navigation
        /// </summary>
        /// <param name="settingNavigationStore"></param>
        /// <param name="mainLayoutSettingViewModel"></param>
        public NavigateLayoutSettingCommand(ISettingNavigationStore settingNavigationStore, IMainLayoutSettingViewModel mainLayoutSettingViewModel)
        {
            _settingNavigationStore = settingNavigationStore;
            _mainLayoutSettingViewModel = mainLayoutSettingViewModel;
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
