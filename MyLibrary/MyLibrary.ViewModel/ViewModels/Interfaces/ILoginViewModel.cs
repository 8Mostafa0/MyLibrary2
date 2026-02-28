namespace MyLibrary.ViewModel.ViewModels.Interfaces
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
