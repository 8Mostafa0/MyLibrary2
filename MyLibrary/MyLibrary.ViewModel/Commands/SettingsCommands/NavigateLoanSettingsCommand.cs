using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateLoanSettingsCommand : CommandBase
    {
        #region Dependencies
        private SettingNavigationStore _navigationStore;
        private MessageBoxStore _messageBoxStore;
        private LoanSettingsViewModel _loanSettingsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view model of setting navigation to loan loan settings
        /// </summary>
        /// <param name="navigationStore"></param>
        public NavigateLoanSettingsCommand(SettingNavigationStore navigationStore, MessageBoxStore messageBoxStore)
        {
            _navigationStore = navigationStore;
            _messageBoxStore = messageBoxStore;
            _loanSettingsViewModel = new LoanSettingsViewModel(_messageBoxStore);
        }
        #endregion

        #region Execution
        public override void Execute(object parameter)
        {
            _navigationStore.CurrentSettingViewModel = _loanSettingsViewModel;
        }
        #endregion
    }
}
