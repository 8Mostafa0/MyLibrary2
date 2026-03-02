using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Commands.LoginCommands;
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

        public static ISecuritySettingsViewModel CreateSecuritySettingsViewModel(ISettingNavigationStore settingNavigationStore, IMessageBoxStore messageBoxStore)
        {
            return new SecuritySettingsViewModel(settingNavigationStore, messageBoxStore);
        }

        public static IMainLayoutSettingViewModel CreateMainLayoutSettingViewModel(ISettingNavigationStore settingNavigationStore, IMessageBoxStore messageBoxStore)
        {
            return new MainLayoutSettingViewModel(settingNavigationStore, messageBoxStore);
        }

        public static ILoanSettingsViewModel CreateLoanSettingsViewModel(ISettingNavigationStore settingNavigationStore, IMessageBoxStore messageBoxStore)
        {
            return new LoanSettingsViewModel(settingNavigationStore, messageBoxStore);
        }

        public static ISettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel();
        }


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
        public static ReservedBooksViewModel LoadViewModel(IReservedBooksStore reservedBooksStore, IModalNavigationStore modalNavigationStore, IClientsStore clientsStore, IBooksStore booksStore, LoanRepository loansRepository, ClientsRepository clientsRepository, ReservedBooksRepository reservedBooksRepository, IMessageBoxStore messageBoxStore)
        {
            ReservedBooksViewModel ViewModel = new ReservedBooksViewModel(reservedBooksStore, modalNavigationStore, clientsStore, booksStore, loansRepository, clientsRepository, reservedBooksRepository, messageBoxStore);
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
        public static IAddEditeReserveBookViewModel LoadViewModel(IModalNavigationStore modalNavigationStore, IReservedBooksStore reservedBooksStore, IClientsStore clientsStore, IBooksStore booksStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository, ClientsRepository clientsRepository, IMessageBoxStore messageBoxStore, ReservedBook reservedBook = null)
        {
            AddEditeReserveBookViewModel ViewModel = new AddEditeReserveBookViewModel(modalNavigationStore, reservedBooksStore, clientsStore, booksStore, loanRepository, reservedBooksRepository, clientsRepository, messageBoxStore, reservedBook);
            ViewModel.LoadBooksCommand.Execute(null);
            ViewModel.LoadClientsCommand.Execute(null);
            ViewModel.SelectedReservedBook = reservedBook is null ? new ReservedBook() { ID = 0, BookId = 0, ClientId = 0 } : reservedBook;
            return ViewModel;
        }


        /// <summary>
        /// Loader for oans view model
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="settingsStore"></param>
        /// <param name="booksRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <returns></returns>
        public static LoansViewModel LoadViewModel(IModalNavigationStore modalNavigationStore, ILoansStore loansStore, IClientsStore clientsStore, IBooksStore booksStore, LoanRepository loanRepository, ISettingsStore settingsStore, BooksRepository booksRepository, ReservedBooksRepository reservedBooksRepository, IMessageBoxStore messageBoxStore)
        {
            LoansViewModel viewModel = new LoansViewModel(modalNavigationStore, loansStore, clientsStore, booksStore, loanRepository, settingsStore, booksRepository, reservedBooksRepository, messageBoxStore);
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
        public static IAddEditeLoanViewModel LoadViewModel(IModalNavigationStore modalNavigationStore, IBooksStore booksStore, IClientsStore clientsStore, ILoansStore loansStore, LoanRepository loanRepository, ISettingsStore settingsStore, BooksRepository booksRepository, ReservedBooksRepository reservedBooksRepository, IMessageBoxStore messageBoxStore, Loan loan = null)
        {
            IAddEditeLoanViewModel ViewModel = new AddEditeLoanViewModel(modalNavigationStore, clientsStore, booksStore, loansStore, loanRepository, settingsStore, booksRepository, reservedBooksRepository, messageBoxStore, loan);
            ViewModel.LoadBooksCommand.Execute(null);
            ViewModel.LoadClientsCommand.Execute(null);
            ViewModel.SelectedLoan = loan is null ? new Loan()
            {
                Id = 0,
                ClientId = 0,
                BookId = 0
            } : loan;
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
        #endregion
    }
}
