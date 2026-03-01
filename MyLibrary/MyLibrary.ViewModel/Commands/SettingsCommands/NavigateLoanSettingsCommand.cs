using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class NavigateLoanSettingsCommand : CommandBase
    {
        #region Dependencies
        private SettingNavigationStore _navigationStore;
        private LoanSettingsViewModel _loanSettingsViewModel = new LoanSettingsViewModel();
        #endregion

        #region Contructor
        /// <summary>
        /// set current view model of setting navigation to loan loan settings
        /// </summary>
        /// <param name="navigationStore"></param>
        public NavigateLoanSettingsCommand(SettingNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
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
