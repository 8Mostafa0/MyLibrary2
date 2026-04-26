using Autofac;
using MyLibrary.Model.DbContexts;
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
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var Container = ContainerConfig.Configure();
            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<MainWindow>();
                IDbContextFactory dbContext = scope.Resolve<IDbContextFactory>();
                await dbContext.CheckDatabaseExistsAsync();
                IMainViewModel mainViewModel = scope.Resolve<IMainViewModel>();
                app.DataContext = mainViewModel;
                app.Show();
            }
        }
    }
}
