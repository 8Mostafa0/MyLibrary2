namespace MyLibrary.ViewModel.ViewModels.Interfaces
{
    public interface IMainViewModel
    {
        IViewModelBase CurrentModalView { get; }
        IViewModelBase CurrentViewModel { get; }
        bool IsModalOpen { get; }
    }
}
