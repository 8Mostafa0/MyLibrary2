using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Stores;
using System.Linq;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class SecuritySettingsViewModel : ViewModelBase
    {
        #region Dependencies
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
                    //MessageBox.Show("لطفا رمز عبور را بیشتر از 5 حرف وارد کنید", "رمز عبور");
                }
            }
        }
        private SettingsStore _settingsStore;
        private SettingNavigationStore _settingNavigationStore;
        #endregion

        #region Commands
        public ICommand ChangeLoginPasswordCommand { get; }
        #endregion

        #region Contructor
        public SecuritySettingsViewModel(SettingNavigationStore settingNavigationStore)
        {
            _settingsStore = new SettingsStore();
            _settingNavigationStore = settingNavigationStore;
            ChangeLoginPasswordCommand = new ChangeLoginPasswordCommand(this, _settingNavigationStore, _settingsStore);
        }

        #endregion
    }
}
