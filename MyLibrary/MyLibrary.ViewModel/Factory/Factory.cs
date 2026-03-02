using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Factory
{
    public static class Factory
    {
        public static IStatusBarViewModel CreateStatusBarViewModel()
        {
            return new StatusBarViewModel();
        }

        //public static INavigationBarViewModel CreateNavigationBarViewModel()
        //{
        //    return new NavigationBarViewModel();
        //}
        //public static ILoginViewModel CreateLoginViewModel()
        //{
        //    return new LoginViewModel();
        //}
        //public static IHomeViewModel CreateHomeViewModel()
        //{
        //    return new HomeViewModel();
        //}
        //public static ILayoutViewModel CreateLayoutViewModel()
        //{
        //    return new LayoutViewModel();
        //}


    }
}
