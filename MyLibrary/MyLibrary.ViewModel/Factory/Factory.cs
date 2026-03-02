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

        /// <summary>
        /// loader method for clients view model
        /// </summary>
        /// <param name="clientStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <returns></returns>
        //public static IClientsViewModel CreateClientsViewModel()
        //{
        //    ClientsViewModel ViewModel = new ClientsViewModel(clientStore, loanRepository, reservedBooksRepository, messageBoxStore);
        //    ViewModel.LoadClientsCommand.Execute(null);
        //    return ViewModel;
        //    return new ClientsViewModel();
        //}


        /// <summary>
        /// Loader Method for Books view model
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="booksRepository"></param>
        /// <returns></returns>
        //public static IBooksViewModel CreateBooksViewModel()
        //{

        //    BooksViewModel ViewModel = new BooksViewModel(booksStore, loanRepository, reservedBooksRepository, booksRepository, messageBoxStore);
        //    ViewModel.LoadBooksCommand.Execute(null);
        //    return ViewModel;
        //}

        //public static ISecuritySettingsViewModel CreateSecuritySettingsViewModel(SettingNavigationStore settingNavigationStore, MessageBoxStore messageBoxStore)
        //{
        //    return new SecuritySettingsViewModel(settingNavigationStore, messageBoxStore);
        //}

        //public static IMainLayoutSettingViewModel CreateMainLayoutSettingViewModel(SettingNavigationStore settingNavigationStore, MessageBoxStore messageBoxStore)
        //{
        //    return new MainLayoutSettingViewModel(settingNavigationStore, messageBoxStore);
        //}

        //public static ILoanSettingsViewModel CreateLoanSettingsViewModel(SettingNavigationStore settingNavigationStore, MessageBoxStore messageBoxStore)
        //{
        //    return new LoanSettingsViewModel(settingNavigationStore, messageBoxStore);
        //}
        //public static ISecuritySettingsViewModel CreateSecuritySettingsViewModel() { }

        //public static ISettingsViewModel CreateSettingsViewModel() { }


        /// <summary>
        /// Loader method for reservedbooks view model
        /// </summary>
        /// <param name="reservedBooksStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loansRepository"></param>
        /// <param name="clientsRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <returns></returns>
        //public static ReservedBooksViewModel LoadViewModel(ReservedBooksStore reservedBooksStore, ModalNavigationStore modalNavigationStore, ClientsStore clientsStore, BooksStore booksStore, LoanRepository loansRepository, ClientsRepository clientsRepository, ReservedBooksRepository reservedBooksRepository, MessageBoxStore messageBoxStore)
        //{
        //    ReservedBooksViewModel ViewModel = new ReservedBooksViewModel(reservedBooksStore, modalNavigationStore, clientsStore, booksStore, loansRepository, clientsRepository, reservedBooksRepository, messageBoxStore);
        //    ViewModel.LoadReservedBooksCommand.Execute(null);
        //    return ViewModel;
        //}
    }
}
