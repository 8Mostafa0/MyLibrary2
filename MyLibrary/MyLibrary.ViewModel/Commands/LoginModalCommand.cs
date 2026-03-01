using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands
{
    public class LoginModalCommand : CommandBase
    {
        #region Dependencies
        private LoginViewModel _loginViewModel;
        private ModalNavigationStore _modalNavigationStore;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of modal navigation to login view
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="dbContextFactory"></param>
        /// <param name="settingsStores"></param>
        public LoginModalCommand(ModalNavigationStore modalNavigationStore, LoanRepository loanRepository, DbContextFactory dbContextFactory, SettingsStore settingsStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _loginViewModel = new LoginViewModel(_modalNavigationStore, loanRepository, dbContextFactory, settingsStore);
        }
        #endregion

        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object parameter)
        {
            _modalNavigationStore.CurrentViewModel = _loginViewModel;
        }
        #endregion
    }
}
