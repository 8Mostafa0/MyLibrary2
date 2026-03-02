using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public class LoginCommand : CommandBase
    {
        #region Dependencies
        private SettingsStore _settinsStore;
        private LoanRepository _loanRepository;
        private LoginViewModel _loginViewModel;
        private DbContextFactory _dbContextFactory;
        private ModalNavigationStore _modalNavigationStore;
        #endregion


        #region Contructor
        /// <summary>
        /// validate password then set modal navigation view to null
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="dbContextFactory"></param>
        /// <param name="settingsStore"></param>
        public LoginCommand(LoginViewModel loginViewModel, ModalNavigationStore modalNavigationStore, DbContextFactory dbContextFactory, SettingsStore settingsStore)
        {
            _loginViewModel = loginViewModel;
            _dbContextFactory = dbContextFactory;
            _modalNavigationStore = modalNavigationStore;
            _settinsStore = settingsStore;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            new CheckDatabaseCommand(_loanRepository, _dbContextFactory).Execute(null);
            if (_loginViewModel.Password == "" || _loginViewModel.Password is null)
            {
                //MessageBox.Show("لطفا مقادیری برای رمز وارد کنید", "خطا");
                return;
            }
            if (_loginViewModel.FirstOpen)
            {
                _settinsStore.SaveNoneHashedPassword(_loginViewModel.Password);
                _modalNavigationStore.Close();
            }
            else
            {
                if (_settinsStore.VerifyPassword(_loginViewModel.Password))
                {
                    _modalNavigationStore.Close();
                }
                else
                {
                    //MessageBox.Show("رمز عبور اشتباه است", "خطا");
                }
            }
        }
        #endregion
    }
}
