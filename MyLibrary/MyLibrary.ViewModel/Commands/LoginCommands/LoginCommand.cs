using MyLibrary.Model.DbContexts;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public class LoginCommand : CommandBase, ILoginCommand
    {
        #region Dependencies
        private ISettingsStore _settinsStore;
        private ILoginViewModel _loginViewModel;
        private IDbContextFactory _dbContextFactory;
        private IMessageBoxStore _messageBoxStore;
        private IModalNavigationStore _modalNavigationStore;
        private ICheckDatabaseCommand _checkDatabaseCommand;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// validate password then set modal navigation view to null
        /// </summary>
        /// <param name="settingsStore"></param>
        /// <param name="loginViewModel"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="dbContextFactory"></param>
        /// <param name="modalNavigationStore"></param>
        public LoginCommand(
            ISettingsStore settingsStore,
            ILoginViewModel loginViewModel,
            IMessageBoxStore messageBoxStore,
            IDbContextFactory dbContextFactory,
            IModalNavigationStore modalNavigationStore,
            ICheckDatabaseCommand checkDatabaseCommand)
        {
            _settinsStore = settingsStore;
            _loginViewModel = loginViewModel;
            _messageBoxStore = messageBoxStore;
            _dbContextFactory = dbContextFactory;
            _modalNavigationStore = modalNavigationStore;
            _checkDatabaseCommand = checkDatabaseCommand;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _checkDatabaseCommand.Execute(null);
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
