using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateLoanSettingsCommand : CommandBase, INavigateLoanSettingsCommand
    {
        #region Dependencies
        private ISettingNavigationStore _navigationStore;
        private IMessageBoxStore _messageBoxStore;
        private ILoanSettingsViewModel _loanSettingsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view model of setting navigation to loan loan settings
        /// </summary>
        /// <param name="navigationStore"></param>
        public NavigateLoanSettingsCommand()
        {
            _navigationStore = ClassFactory.CreateSettingNavigationStore();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _loanSettingsViewModel = ClassFactory.CreateLoanSettingsViewModel();
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
