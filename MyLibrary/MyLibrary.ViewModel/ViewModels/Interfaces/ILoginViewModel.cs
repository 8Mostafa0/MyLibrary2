using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface ILoginViewModel : IViewModelBase
    {
        ICommand CloseAppCommand { get; }
        bool FirstOpen { get; }
        ICommand LoginCommand { get; }
        string Password { get; set; }
        string Title { get; set; }
    }
}