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




    }
}
