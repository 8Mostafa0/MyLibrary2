using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateLoanSettingsCommand : CommandBase, INavigateLoanSettingsCommand
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private ISettingNavigationStore _navigationStore;
        private ILoanSettingsViewModel _loanSettingsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// set current view model of setting navigation to loan loan settings
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="loanSettingsViewModel"></param>
        public NavigateLoanSettingsCommand(
            IMessageBoxStore messageBoxStore,
            ISettingNavigationStore navigationStore,
            ILoanSettingsViewModel loanSettingsViewModel)
        {
            _navigationStore = navigationStore;
            _messageBoxStore = messageBoxStore;
            _loanSettingsViewModel = loanSettingsViewModel;
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
