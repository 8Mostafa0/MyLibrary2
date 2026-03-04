using MyLibrary.ViewModel.Commands.LoginCommands;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface ILoginViewModel : IViewModelBase
    {

        ICloseAppCommand CloseAppCommand { get; }
        ILoginCommand LoginCommand { get; }
        string Password { get; set; }
        string Title { get; set; }
    }
}