using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        #region Dependencies
        private ISettingNavigationStore _settingNavigationStore;
        private IMessageBoxStore _messageBoxStore;

        public IViewModelBase CurrentSettingViewModel => _settingNavigationStore.CurrentSettingViewModel;

        #endregion

        #region Commands
        public ICommand NavigateLayoutSettingCommand { get; }
        public INavigateLoanSettingsCommand NavigateLoanSettingsCommand { get; }
        public ICommand NavigateSecuritySettingsCommand { get; }
        #endregion

        #region Contructor
        public SettingsViewModel()
        {
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _settingNavigationStore = ClassFactory.CreateSettingNavigationStore();
            _settingNavigationStore.SettingViewModelChanged += OnSettingViewModelChanged;
            NavigateLayoutSettingCommand = new NavigateLayoutSettingCommand(_settingNavigationStore);
            NavigateLoanSettingsCommand = ClassFactory.CreateNavigateLoanSettingsCommand();
            NavigateSecuritySettingsCommand = new NavigateSecuritySettingsCommand(_settingNavigationStore, _messageBoxStore);
        }

        #endregion

        #region Methods
        private void OnSettingViewModelChanged()
        {
            OnProperychanged(nameof(CurrentSettingViewModel));
        }
        #endregion

    }
}
