using MyLibrary.Model.DbContexts;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public class LoginCommand : CommandBase
    {
        #region Dependencies
        private ISettingsStore _settinsStore;
        private ILoginViewModel _loginViewModel;
        private DbContextFactory _dbContextFactory;
        private IModalNavigationStore _modalNavigationStore;
        private IMessageBoxStore _messageBoxStore;
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
        public LoginCommand(ILoginViewModel loginViewModel, IModalNavigationStore modalNavigationStore, DbContextFactory dbContextFactory, ISettingsStore settingsStore, IMessageBoxStore messageBoxStore)
        {
            _loginViewModel = loginViewModel;
            _dbContextFactory = dbContextFactory;
            _modalNavigationStore = modalNavigationStore;
            _settinsStore = settingsStore;
            _messageBoxStore = messageBoxStore;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            new CheckDatabaseCommand(_dbContextFactory).Execute(null);
            if (_loginViewModel.Password == "" || _loginViewModel.Password is null)
            {
                _messageBoxStore.Show("لطفا مقادیری برای رمز وارد کنید", "خطا");
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
                    _messageBoxStore.Show("رمز عبور اشتباه است", "خطا");
                }
            }
        }
        #endregion
    }
}
