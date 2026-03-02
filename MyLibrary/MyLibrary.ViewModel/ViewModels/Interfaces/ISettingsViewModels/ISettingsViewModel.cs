using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public interface ISettingsViewModel : IViewModelBase
    {
        IViewModelBase CurrentSettingViewModel { get; }
        ICommand NavigateLayoutSettingCommand { get; }
        ICommand NavigateLoanSettingsCommand { get; }
        ICommand NavigateSecuritySettingsCommand { get; }
    }
}