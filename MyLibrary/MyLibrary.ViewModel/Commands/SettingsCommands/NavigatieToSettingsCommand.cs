using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateToSettingsCommand : CommandBase
    {
        #region Dependencies
        private NavigationStore _navigationStore;
        private SettingsViewModel _settingsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of main navigation to to settings view
        /// </summary>
        private SettingNavigationStore _settingsNavigationStore = new SettingNavigationStore();
        public NavigateToSettingsCommand(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _settingsViewModel = new SettingsViewModel(_settingsNavigationStore);
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object? parameter)
        {
            _navigationStore.ContentScreen = _settingsViewModel;
        }
        #endregion
    }
}
