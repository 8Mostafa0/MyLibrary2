using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class NavigateLoansCommand : CommandBase, INavigateLoansCommand
    {
        #region Dependencies
        private ILoansViewModel _loansViewModel;
        private INavigationStore _navigationStore;
        #endregion


        #region Contructor
        /// <summary>
        /// set content of navigate to loans view model
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="loansViewModel"></param>
        public NavigateLoansCommand(
            INavigationStore navigationStore,
            ILoansViewModel loansViewModel)
        {
            _navigationStore = navigationStore;
            _loansViewModel = loansViewModel;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _navigationStore.ContentScreen = _loansViewModel;
        }
        #endregion
    }
}
