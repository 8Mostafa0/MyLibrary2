using MyLibrary.Model.Repositories;
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
            NavigationStore navigationStore = new NavigationStore();
            ClientsStore clientsStore = new ClientsStore();
            LoansStore loansStore = new LoansStore();
            BooksStore booksStore = new BooksStore();
            SettingsStore settingsStore = new SettingsStore();
            LoanRepository loanRepository = new LoanRepository();
            ReservedBooksStore reservedBooksStore = new ReservedBooksStore();
            ReservedBooksRepository reservedBooksRepository = new ReservedBooksRepository();
            BooksRepository booksRepository = new BooksRepository();
            ClientsRepository clientsRepository = new ClientsRepository();
            navigationStore.ContentScreen = new HomeViewModel(clientsStore, booksStore, loansStore);
            navigationStore.StatusBarViewModel = new StatusBarViewModel();

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
                clientsRepository
                );

            LayoutViewModel layoutViewModel = new LayoutViewModel(navigationStore);
            MainWindow _ = new MainWindow()
            {
                DataContext = new MainViewModel(layoutViewModel)
            };
            MainWindow.Show();
        }
    }
}
