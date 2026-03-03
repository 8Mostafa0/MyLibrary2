using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Commands.LoginCommands;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;

namespace MyLibrary.ViewModel.Factory
{
    public static class ClassFactory
    {
        #region ViewModels

        public static IStatusBarViewModel CreateStatusBarViewModel()
        {
            return new StatusBarViewModel();
        }

        public static INavigationBarViewModel CreateNavigationBarViewModel()
        {
            return new NavigationBarViewModel();
        }
        public static ILoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel();
        }
        public static IHomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel();
        }
        public static ILayoutViewModel CreateLayoutViewModel()
        {
            return new LayoutViewModel();
        }
        public static IMainViewModel CreateMainViewModel()
        {
            return new MainViewModel();
        }

        /// <summary>
        /// loader method for clients view model
        /// </summary>
        /// <param name="clientStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <returns></returns>
        public static IClientsViewModel CreateClientsViewModel()
        {
            IClientsViewModel ViewModel = new ClientsViewModel();
            ViewModel.LoadClientsCommand.Execute(null);
            return ViewModel;
        }


        /// <summary>
        /// Loader Method for Books view model
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="booksRepository"></param>
        /// <returns></returns>
        public static IBooksViewModel CreateBooksViewModel()
        {
            BooksViewModel ViewModel = new BooksViewModel();
            ViewModel.LoadBooksCommand.Execute(null);
            return ViewModel;
        }

        public static ISecuritySettingsViewModel CreateSecuritySettingsViewModel()
        {
            return new SecuritySettingsViewModel();
        }

        public static IMainLayoutSettingViewModel CreateMainLayoutSettingViewModel()
        {
            return new MainLayoutSettingViewModel();
        }

        public static ILoanSettingsViewModel CreateLoanSettingsViewModel()
        {
            return new LoanSettingsViewModel();
        }

        public static ISettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel();
        }

        /// <summary>
        /// Loader method for reservedbooks view model
        /// </summary>
        /// <returns></returns>
        public static IReservedBooksViewModel CreateReservedBooksViewModel()
        {
            ReservedBooksViewModel ViewModel = new ReservedBooksViewModel();
            ViewModel.LoadReservedBooksCommand.Execute(null);
            return ViewModel;
        }



        /// <summary>
        /// Loader for add edite reservedbook view model
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="clientsRepository"></param>
        /// <param name="reservedBook"></param>
        /// <returns></returns>
        public static IAddEditeReserveBookViewModel CreateAddEditeReserveBookViewModel()
        {
            AddEditeReserveBookViewModel ViewModel = new AddEditeReserveBookViewModel();
            ViewModel.LoadBooksCommand.Execute(null);
            ViewModel.LoadClientsCommand.Execute(null);
            //ViewModel.SelectedReservedBook = reservedBook is null ? new ReservedBook() { ID = 0, BookId = 0, ClientId = 0 } : reservedBook;
            return ViewModel;
        }


        /// <summary>
        /// Loader for oans view model
        /// </summary>
        /// <returns></returns>
        public static ILoansViewModel CreateLoansViewModel()
        {
            LoansViewModel viewModel = new LoansViewModel();
            viewModel.LoadLoansCommand.Execute(null);
            return viewModel;
        }



        /// <summary>
        /// loader for addedite view model
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="settingsStore"></param>
        /// <param name="booksRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="loan"></param>
        /// <returns></returns>
        public static IAddEditeLoanViewModel CreateAddEditeLoanViewModel()
        {
            IAddEditeLoanViewModel ViewModel = new AddEditeLoanViewModel();
            ViewModel.LoadBooksCommand.Execute(null);
            ViewModel.LoadClientsCommand.Execute(null);
            return ViewModel;

        }
        #endregion

        #region Commands

        public static INavigateHomeScreenCommand CreateNavigateHomeScreenCommand()
        {
            return new NavigateHomeScreenCommand();
        }

        public static ILoginModalCommand CreateLoginModalCommand()
        {
            return new LoginModalCommand();
        }

        public static ICheckDatabaseCommand CreateCheckDatabaseCommand()
        {
            return new CheckDatabaseCommand();
        }

        public static ICloseAppCommand CreateCloseAppCommand()
        {
            return new CloseAppCommand();
        }
        public static ILoginCommand CreateLoginCommand()
        {
            return new LoginCommand();
        }
        public static INavigateClientScreenCommand CreateNavigateClientScreenCommand()
        {
            return new NavigateClientScreenCommand();
        }
        public static INavigateBooksCommand CreateNavigateBooksScreenCommand()
        {
            return new NavigateBooksCommand();
        }
        public static INavigateLoansCommand CreateNavigateLoansCommand()
        {
            return new NavigateLoansCommand();
        }
        public static IShowLoanModalCommand CreateShowLoanModalCommand()
        {
            return new ShowLoanModalCommand();
        }
        public static ILoadLoansCommand CreateLoadLoansCommand()
        {
            return new LoadLoansCommand();
        }
        public static IShowEditLoanViewModel CreateShowEditLoanViewModel()
        {
            return new ShowEditLoanViewModel();
        }
        public static ISortLoansListCommand CreateSortLoansListCommand()
        {
            return new SortLoansListCommand();
        }
        public static IReturnedLoanCommand CreateReturnedLoanCommand()
        {
            return new ReturnedLoanCommand();
        }

        internal static IReloadLoansListCommand CreateReloadLoansListCommand()
        {
            return new ReloadLoansListCommand();
        }
        public static ISearchBookCommand CreateSearchBookCommand()
        {
            return new SearchBookCommand();
        }
        public static IOrderBooksByStateCommand CreateOrderBooksCommand()
        {
            return new OrderBooksByStateCommand();
        }
        public static INavigateReservedBooksCommand CreateNavigateReservedBooksCommand()
        {
            return new NavigateReservedBooksCommand();
        }
        public static INavigateToSettingsCommand CreateNavigateToSettingsCommand()
        {
            return new NavigateToSettingsCommand();
        }
        public static ILoadBooksCommand CreateLoadBooksCommand()
        {
            return new LoadBooksCommand();
        }
        public static ILoadClientsCommand CreateLoadClientsCommand()
        {
            return new LoadClientsCommand();
        }

        public static ICloseModalCommand CreateCloseModalCommand()
        {
            return new CloseModalCommand();
        }
        public static ISearchBookNameCommand CreateSearchBookNameCommand()
        {
            return new SearchBookNameCommand();
        }
        public static ISearchClientNameCommand CreateSearchClientNameCommand()
        {
            return new SearchClientNameCommand();
        }

        public static IOrderBooksBySubjectCommand CreateOrderBooksBySubjectCommand()
        {
            return new OrderBooksBySubjectCommand();
        }
        public static ISaveReservationDataCommand CreateSaveReservationDataCommand()
        {
            return new SaveReservationDataCommand();
        }
        public static ISaveLoanDataCommand CreateSaveLoanDataCommand()
        {
            return new SaveLoanDataCommand();
        }
        public static ILoadReservedBooksCommand CreateLoadReservedBooksCommand()
        {
            return new LoadReservedBooksCommand();
        }
        public static IEditeReservBookCommand CreateEditeReservBookCommand()
        {
            return new EditeReservBookCommand();
        }
        public static IAddNewReservBookCommand CreateAddNewReservBookCommand()
        {
            return new AddNewReservBookCommand();
        }
        public static IRemoveReservBookCommand CreateRemoveReservBookCommand()
        {
            return new RemoveReservBookCommand();
        }
        public static IResetReservBookCommand CreateResetReservBookCommand()
        {
            return new ResetReservBookCommand();
        }
        public static ISearchBookNameInReservedBookCommand CreateSearchBookNameInReservedBookCommand()
        {
            return new SearchBookNameInReservedBookCommand();
        }
        public static IAddNewBookCommand CreateAddNewBookCommand()
        {
            return new AddNewBookCommand();
        }
        public static IEditBookCommand CreateEditBookCommand()
        {
            return new EditBookCommand();
        }
        public static IDeleteBookCommand CreateDeleteBookCommand()
        {
            return new DeleteBookCommand();
        }
        public static IReloadBooksCommand CreateReloadBooksCommand()
        {
            return new ReloadBooksCommand();
        }
        #region ClientsCommands
        public static IReloadClientsCommand CreateReloadClientsCommand()
        {
            return new ReloadClientsCommand();
        }
        public static IDeleteClientCommand CreateDeleteClientCommand()
        {
            return new DeleteClientCommand();
        }
        public static IAddNewClientCommand CreateAddNewClientCommand()
        {
            return new AddNewClientCommand();
        }
        public static IOrderClientsCommand CreateOrderClientsCommand()
        {
            return new OrderClientsCommand();
        }
        public static IEditClientCommand CreateEditClientCommand()
        {
            return new EditClientCommand();
        }
        #endregion
        #endregion

        #region Stores

        public static INavigationStore CreateNavigationStore()
        {
            return new NavigationStore();
        }

        public static IBooksStore CreateBooksStore()
        {
            return new BooksStore();
        }

        public static IClientsStore CreateClientsStore()
        {
            return new ClientsStore();
        }
        public static ILoansStore CreateLoansStore()
        {
            return new LoansStore();
        }
        public static IModalNavigationStore CreateModalNavigationStore()
        {
            return new ModalNavigationStore();
        }
        public static IMessageBoxStore CreateMessageBoxStore()
        {
            return new MessageBoxStore();
        }

        public static IReservedBooksStore CreateReservedBooksStore()
        {
            return new ReservedBooksStore();
        }
        public static ISettingNavigationStore CreateSettingNavigationStore()
        {
            return new SettingNavigationStore();
        }
        public static ISettingsStore CreateSettingsStore()
        {
            return new SettingsStore();
        }
        public static ITimeStore CreateTimeStore()
        {
            return new TimeStore();
        }
        #endregion

        #region Repositories
        public static IDbContextFactory CreateDbContextFactory()
        {
            return new DbContextFactory();
        }

        public static IBooksRepository CreateBooksRepository()
        {
            return new BooksRepository();
        }

        public static IClientsRepository CreateClientsRepository()
        {
            return new ClientsRepository();
        }

        public static ILoanRepository CreateLoanRepository()
        {
            return new LoanRepository();
        }
        public static IReservedBooksRepository CreateReservedBooksRepository()
        {
            return new ReservedBooksRepository();
        }

        #endregion
    }
}
