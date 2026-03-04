using MyLibrary.Model.DbContexts;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public class LoginCommand : CommandBase, ILoginCommand
    {
        #region Dependencies
        private ISettingsStore _settingsStore;
        private ILoginStore _loginStore;
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
        /// <param name="loginStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="dbContextFactory"></param>
        /// <param name="modalNavigationStore"></param>
        public LoginCommand(
            ISettingsStore settingsStore,
            ILoginStore loginStore,
            IMessageBoxStore messageBoxStore,
            IDbContextFactory dbContextFactory,
            IModalNavigationStore modalNavigationStore,
            ICheckDatabaseCommand checkDatabaseCommand)
        {
            _settingsStore = settingsStore;
            _loginStore = loginStore;
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
            if (_loginStore.Password == "" || _loginStore.Password is null)
            {
                _messageBoxStore.Show("لطفا مقادیری برای رمز وارد کنید", "خطا");
                return;
            }
            if (_settingsStore.GetHashedPassword() == null)
            {
                _settingsStore.SaveNoneHashedPassword(_loginStore.Password);
                _modalNavigationStore.Close();
            }
            else
            {
                if (_settingsStore.VerifyPassword(_loginStore.Password))
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
