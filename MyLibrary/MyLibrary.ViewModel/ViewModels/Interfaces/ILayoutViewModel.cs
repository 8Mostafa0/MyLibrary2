namespace MyLibrary.ViewModel.ViewModels.Interfaces
{
    public interface ILayoutViewModel : IViewModelBase
    {
        IViewModelBase contentViewModel { get; }
        INavigationBarViewModel MainContentViewModel { get; }
        IStatusBarViewModel StatusBarViewModel { get; }
    }
}
