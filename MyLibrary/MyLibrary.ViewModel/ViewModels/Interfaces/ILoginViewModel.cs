using MyLibrary.ViewModel.Commands.LoginCommands;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface ILoginViewModel : IViewModelBase
    {
        ICloseAppCommand CloseAppCommand { get; }
        bool FirstOpen { get; }
        ILoginCommand LoginCommand { get; }
        string Password { get; set; }
        string Title { get; set; }
    }
}