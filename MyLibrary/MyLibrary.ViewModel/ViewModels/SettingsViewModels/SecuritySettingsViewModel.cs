using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Stores;
using System.Linq;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class SecuritySettingsViewModel : ViewModelBase, ISecuritySettingsViewModel
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private ILoginStore _loginStore;
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (!(value == "") || value.Count() >= 5)
                {
                    _password = value;
                    _loginStore.Password = value;
                    OnProperychanged(nameof(Password));
                }
                else
                {
                    _password = null;
                    _messageBoxStore.Show("لطفا رمز عبور را بیشتر از 5 حرف وارد کنید", "رمز عبور");
                }
            }
        }
        private ISettingsStore _settingsStore;
        private ISettingNavigationStore _settingNavigationStore;
        #endregion

        #region Commands
        public IChangeLoginPasswordCommand ChangeLoginPasswordCommand { get; }
        #endregion

        #region Contructor
        public SecuritySettingsViewModel(
            ILoginStore loginStore,
            ISettingsStore settingsStore,
            IMessageBoxStore messageBoxStore,
            ISettingNavigationStore settingNavigationStore,
            IChangeLoginPasswordCommand changeLoginPasswordCommand)
        {
            _loginStore = loginStore;
            _settingsStore = settingsStore;
            _messageBoxStore = messageBoxStore;
            _settingNavigationStore = settingNavigationStore;
            ChangeLoginPasswordCommand = changeLoginPasswordCommand;
        }

        #endregion
    }
}
