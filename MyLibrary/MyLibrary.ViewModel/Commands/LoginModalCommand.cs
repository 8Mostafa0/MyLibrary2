using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands
{
    public class LoginModalCommand : CommandBase, ILoginModalCommand
    {
        #region Dependencies
        private ILoginViewModel _loginViewModel;
        private IModalNavigationStore _modalNavigationStore;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of modal navigation to login view
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="loginViewModel"></param>
        public LoginModalCommand(
            IModalNavigationStore modalNavigationStore,
            ILoginViewModel loginViewModel
            )
        {
            _modalNavigationStore = modalNavigationStore;
            _loginViewModel = loginViewModel;
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
