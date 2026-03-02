using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Factory
{
    public static class Factory
    {
        public static IStatusBarViewModel CreateStatusBarViewModel()
        {
            return new StatusBarViewModel();
        }
    }
}
