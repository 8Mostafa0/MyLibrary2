namespace MyLibrary.ViewModel.ViewModels
{
    public interface IMainViewModel
    {
        IViewModelBase CurrentMessageBox { get; }
        IViewModelBase CurrentModalView { get; }
        IViewModelBase CurrentViewModel { get; }
        bool IsMessageBoxOpen { get; }
        bool IsModalOpen { get; }
    }
}