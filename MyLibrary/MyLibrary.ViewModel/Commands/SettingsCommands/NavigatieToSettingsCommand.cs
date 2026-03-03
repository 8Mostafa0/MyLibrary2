using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateToSettingsCommand : CommandBase, INavigateToSettingsCommand
    {
        #region Dependencies
        private INavigationStore _navigationStore;
        private ISettingsViewModel _settingsViewModel;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of main navigation to to settings view
        /// </summary>
        public NavigateToSettingsCommand(INavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _settingsViewModel = ClassFactory.CreateSettingsViewModel();
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
