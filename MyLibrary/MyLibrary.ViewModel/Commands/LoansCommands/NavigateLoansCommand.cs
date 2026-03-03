using MyLibrary.ViewModel.Factory;
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
        /// <param name="modalNavigationStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="settingsStore"></param>
        /// <param name="booksRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public NavigateLoansCommand()
        {
            _navigationStore = ClassFactory.CreateNavigationStore();
            _loansViewModel = ClassFactory.CreateLoansViewModel();
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
