using Autofac;
using MyLibrary.ViewModel;
using MyLibrary.ViewModel.ViewModels;
using System;
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
                if (Container.IsRegistered<ILayoutViewModel>())
                {
                    Console.WriteLine("IMyService is registered");
                }
                else
                {
                    Console.WriteLine("IMyService is NOT registered");
                }
                var app = scope.Resolve<MainWindow>();
                IMainViewModel mainViewModel = scope.Resolve<IMainViewModel>();
                app.DataContext = mainViewModel;
                app.Show();
            }
        }
    }
}
