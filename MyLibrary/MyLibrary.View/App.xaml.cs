using Autofac;
using MyLibrary.ViewModel;
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
            var Container = ContainerConfig.Configure();
            using (var scope = Container.BeginLifetimeScope())
            {
                var navigationStore = scope.Resolve<INavigationStore>();
                navigationStore.ContentScreen = scope.Resolve<IHomeViewModel>();
                navigationStore.StatusBarViewModel = scope.Resolve<IStatusBarViewModel>();
                navigationStore.MainContentViewModel = scope.Resolve<INavigationBarViewModel>();
                var layoutViewModel = scope.Resolve<ILayoutViewModel>();
                MainWindow _ = new MainWindow()
                {
                    DataContext = scope.Resolve<IMainViewModel>()
                };
                MainWindow.Show();
            }
        }
    }
}
