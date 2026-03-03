using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using System.Linq;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class SecuritySettingsViewModel : ViewModelBase, ISecuritySettingsViewModel
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (!(value == "") || value.Count() >= 5)
                {
                    _password = value;
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
        public SecuritySettingsViewModel()
        {
            ChangeLoginPasswordCommand = ClassFactory.CreateChangeLoginPasswordCommand(this);
            _settingsStore = ClassFactory.CreateSettingsStore();
            _settingNavigationStore = ClassFactory.CreateSettingNavigationStore();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
        }

        #endregion
    }
}
