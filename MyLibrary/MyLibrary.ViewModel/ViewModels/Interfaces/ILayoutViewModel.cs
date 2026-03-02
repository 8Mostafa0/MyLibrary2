namespace MyLibrary.ViewModel.ViewModels
{
    public interface ILayoutViewModel : IViewModelBase
    {
        IViewModelBase contentViewModel { get; }
        INavigationBarViewModel MainContentViewModel { get; }
        IStatusBarViewModel StatusBarViewModel { get; }
    }
}