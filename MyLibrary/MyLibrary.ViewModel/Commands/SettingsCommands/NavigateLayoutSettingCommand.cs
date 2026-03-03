using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateLayoutSettingCommand : CommandBase, INavigateLayoutSettingCommand
    {
        #region Dependencies
        private ISettingNavigationStore _settingNavigationStore;
        private IMainLayoutSettingViewModel _mainLayoutSettingViewModel = ClassFactory.CreateMainLayoutSettingViewModel();
        #endregion

        #region Contructor
        /// <summary>
        /// set setting view of setting navigation to main setting navigation
        /// </summary>
        public NavigateLayoutSettingCommand()
        {
            _settingNavigationStore = ClassFactory.CreateSettingNavigationStore();

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
