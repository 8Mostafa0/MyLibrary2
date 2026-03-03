using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;
using System.Linq;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class ChangeLoginPasswordCommand : CommandBase, IChangeLoginPasswordCommand
    {
        #region Dependencies
        private ISettingsStore _settingsStore;
        private ISettingNavigationStore _settingNavigationStore;
        private ISecuritySettingsViewModel _securitySettingViewModel;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// save validated password to registry
        /// </summary>
        public ChangeLoginPasswordCommand(ISecuritySettingsViewModel securitySettingsViewModel)
        {
            _securitySettingViewModel = securitySettingsViewModel;
            _settingsStore = ClassFactory.CreateSettingsStore();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _settingNavigationStore = ClassFactory.CreateSettingNavigationStore();
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            if (_securitySettingViewModel.Password == "" || _securitySettingViewModel.Password is null || _securitySettingViewModel.Password.Count() < 5)
            {
                _messageBoxStore.Show("لطفا رمز عبور را بیشتر از 5 حرف وارد کنید", "رمز عبور");
                return;
            }
            _settingsStore.SaveNoneHashedPassword(_securitySettingViewModel.Password);
            _settingNavigationStore.CurrentSettingViewModel = null;
            _messageBoxStore.Show("رمز عبور با موفقیت تغییر یافت", "رمز عبور");
        }
        #endregion
    }
}
