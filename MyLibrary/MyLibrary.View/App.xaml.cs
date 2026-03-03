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


            navigationStore.MainContentViewModel = new NavigationBarViewModel(
                navigationStore,
                reservedBooksStore,
                clientsStore,
                booksStore,
                loansStore,
                loanRepository,
                settingsStore,
                booksRepository,
                reservedBooksRepository,
                clientsRepository,
                messageBoxStore,
                modalNavigationStore
                );

            LayoutViewModel layoutViewModel = new LayoutViewModel(navigationStore);
            MainWindow _ = new MainWindow()
            {
                DataContext = new MainViewModel(layoutViewModel, modalNavigationStore, messageBoxStore)
            };
            MainWindow.Show();
        }
    }
}
