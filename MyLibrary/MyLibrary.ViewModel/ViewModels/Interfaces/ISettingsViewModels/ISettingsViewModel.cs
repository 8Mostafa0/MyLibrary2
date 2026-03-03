using MyLibrary.ViewModel.Commands.SettingsCommands;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public interface ISettingsViewModel : IViewModelBase
    {
        IViewModelBase CurrentSettingViewModel { get; }
        INavigateLayoutSettingCommand NavigateLayoutSettingCommand { get; }
        INavigateLoanSettingsCommand NavigateLoanSettingsCommand { get; }
        INavigateSecuritySettingsCommand NavigateSecuritySettingsCommand { get; }
    }
}