using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateToSettingsCommand : CommandBase
    {
        #region Dependencies
        private NavigationStore _navigationStore;
        private ISettingsViewModel _settingsViewModel;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of main navigation to to settings view
        /// </summary>
        private SettingNavigationStore _settingsNavigationStore = new SettingNavigationStore();
        public NavigateToSettingsCommand(NavigationStore navigationStore, IMessageBoxStore messageBoxStore)
        {
            _navigationStore = navigationStore;
            _messageBoxStore = messageBoxStore;
            _settingsViewModel = new SettingsViewModel(_settingsNavigationStore, _messageBoxStore);
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _navigationStore.ContentScreen = _settingsViewModel;
        }
        #endregion
    }
}
