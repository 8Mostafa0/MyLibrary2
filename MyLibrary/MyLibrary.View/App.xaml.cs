using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System.Windows;

namespace MyLibrary.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            INavigationStore navigationStore = ClassFactory.CreateNavigationStore();
            navigationStore.ContentScreen = ClassFactory.CreateHomeViewModel();
            navigationStore.StatusBarViewModel = ClassFactory.CreateStatusBarViewModel();
            navigationStore.MainContentViewModel = ClassFactory.CreateNavigationBarViewModel();
            ILayoutViewModel layoutViewModel = ClassFactory.CreateLayoutViewModel();
            MainWindow _ = new MainWindow()
            {
                DataContext = ClassFactory.CreateMainViewModel()
            };
            MainWindow.Show();
        }
    }
}
