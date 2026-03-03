using MyLibrary.ViewModel.Commands.SettingsCommands;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public interface ISecuritySettingsViewModel : IViewModelBase
    {
        IChangeLoginPasswordCommand ChangeLoginPasswordCommand { get; }
        string Password { get; set; }
    }
}