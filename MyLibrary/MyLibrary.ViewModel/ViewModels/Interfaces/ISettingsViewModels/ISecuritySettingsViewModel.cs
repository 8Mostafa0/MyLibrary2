using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public interface ISecuritySettingsViewModel : IViewModelBase
    {
        ICommand ChangeLoginPasswordCommand { get; }
        string Password { get; set; }
    }
}