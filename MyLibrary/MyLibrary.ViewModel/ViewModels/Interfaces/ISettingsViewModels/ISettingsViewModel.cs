using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public interface ISettingsViewModel : IViewModelBase
    {
        IViewModelBase CurrentSettingViewModel { get; }
        ICommand NavigateLayoutSettingCommand { get; }
        INavigateLoanSettingsCommand NavigateLoanSettingsCommand { get; }
        ICommand NavigateSecuritySettingsCommand { get; }
    }
}