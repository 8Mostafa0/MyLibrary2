using Autofac;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System.Reflection;
namespace MyLibrary.ViewModel
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            #region ViewModels
            builder.RegisterType<HomeViewModel>().As<IHomeViewModel>().SingleInstance();
            builder.RegisterType<NavigationStore>().As<INavigationStore>().SingleInstance();
            builder.RegisterType<ClientsStore>().As<IClientsStore>().SingleInstance();
            builder.RegisterType<StatusBarViewModel>().As<IStatusBarViewModel>().SingleInstance();
            builder.RegisterType<NavigationBarViewModel>().As<INavigationBarViewModel>().SingleInstance();
            builder.RegisterType<LayoutViewModel>().As<ILayoutViewModel>().SingleInstance();

            #endregion
            #region Commands
            #endregion
            #region Stores
            builder.RegisterType<BooksStore>().As<IBooksStore>().SingleInstance();
            builder.RegisterType<LoansStore>().As<ILoansStore>().SingleInstance();
            builder.RegisterType<SettingsStore>().As<ISettingsStore>().SingleInstance();
            #endregion
            #region Repositorys
            builder.RegisterType<ClientsRepository>().As<IClientsRepository>().SingleInstance();
            #endregion
            return builder.Build();
        }
    }
}
