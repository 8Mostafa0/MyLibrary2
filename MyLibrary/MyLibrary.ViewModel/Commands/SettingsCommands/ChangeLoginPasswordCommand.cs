using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;
using System.Linq;

namespace MyLibrary.ViewModel.Commands.SettingsCommands
{
    public class ChangeLoginPasswordCommand : CommandBase
    {
        #region Dependencies
        private SettingsStore _settingsStore;
        private SettingNavigationStore _settingNavigationStore;
        private SecuritySettingsViewModel _securitySettingViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// save validated password to registry
        /// </summary>
        /// <param name="securitySettingsViewModel"></param>
        /// <param name="settingNavigationStore"></param>
        /// <param name="settingsStore"></param>
        public ChangeLoginPasswordCommand(SecuritySettingsViewModel securitySettingsViewModel, SettingNavigationStore settingNavigationStore, SettingsStore settingsStore)
        {
            _settingsStore = settingsStore;
            _settingNavigationStore = settingNavigationStore;
            _securitySettingViewModel = securitySettingsViewModel;
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
                //MessageBox.Show("لطفا رمز عبور را بیشتر از 5 حرف وارد کنید", "رمز عبور");
                return;
            }
            _settingsStore.SaveNoneHashedPassword(_securitySettingViewModel.Password);
            _settingNavigationStore.CurrentSettingViewModel = null;
            //MessageBox.Show("رمز عبور با موفقیت تغییر یافت", "رمز عبور");
        }
        #endregion
    }
}
