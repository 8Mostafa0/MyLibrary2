using MyLibrary.ViewModel.Stores;
using System.Linq;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class ChangeLoginPasswordCommand : CommandBase, IChangeLoginPasswordCommand
    {
        #region Dependencies
        private ILoginStore _loginStore;
        private ISettingsStore _settingsStore;
        private IMessageBoxStore _messageBoxStore;
        private ISettingNavigationStore _settingNavigationStore;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// save validated password to registry
        /// </summary>
        /// <param name="settingsStore"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="settingNavigationStore"></param>
        public ChangeLoginPasswordCommand(
            ILoginStore loginStore,
            ISettingsStore settingsStore,
            IMessageBoxStore messageBoxStore,
            ISettingNavigationStore settingNavigationStore
            )
        {
            _loginStore = loginStore;
            _settingsStore = settingsStore;
            _messageBoxStore = messageBoxStore;
            _settingNavigationStore = settingNavigationStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            if (_loginStore.Password == "" || _loginStore.Password is null || _loginStore.Password.Count() < 5)
            {
                _messageBoxStore.Show("لطفا رمز عبور را بیشتر از 5 حرف وارد کنید", "رمز عبور");
                return;
            }
            _settingsStore.SaveNoneHashedPassword(_loginStore.Password);
            _settingNavigationStore.CurrentSettingViewModel = null;
            _messageBoxStore.Show("رمز عبور با موفقیت تغییر یافت", "رمز عبور");
        }
        #endregion
    }
}
