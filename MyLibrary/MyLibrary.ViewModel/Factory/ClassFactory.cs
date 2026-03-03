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
using MyLibrary.ViewModel.ViewModels.MessageBoxViewModel;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;
using System.Windows.Input;

namespace MyLibrary.ViewModel.Factory
{
    public static class ClassFactory
    {
        #region ViewModels
        /// <summary>
        /// Creates a new instance of an object that implements the navigation bar view model interface.
        /// </summary>
        /// <returns>An object implementing <see cref="INavigationBarViewModel"/> representing the navigation bar view model.</returns>
        public static INavigationBarViewModel CreateNavigationBarViewModel()
        {
            return new NavigationBarViewModel();
        }
        /// <summary>
        /// status bar view model
        /// </summary>
        /// <returns></returns>
        public static IStatusBarViewModel CreateStatusBarViewModel()
        {
            return new StatusBarViewModel();
        }
        /// <summary>
        /// Creates a new instance of an object that implements the ILoginViewModel interface.
        /// </summary>
        /// <returns>An ILoginViewModel instance representing a login view model.</returns>
        public static ILoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel();
        }
        /// <summary>
        /// Creates a new instance of an object that implements the <see cref="IHomeViewModel"/> interface.
        /// </summary>
        /// <returns>An <see cref="IHomeViewModel"/> instance representing the home view model.</returns>
        public static IHomeViewModel CreateHomeViewModel()
        {
            return new HomeViewModel();
        }

        /// <summary>
        /// Creates a new instance of an object that implements the <see cref="ILayoutViewModel"/> interface.
        /// </summary>
        /// <returns>An <see cref="ILayoutViewModel"/> instance representing the layout view model.</returns>
        public static ILayoutViewModel CreateLayoutViewModel()
        {
            return new LayoutViewModel();
        }
        /// <summary>
        /// Creates a new instance of the main view model for the application.
        /// </summary>
        /// <returns>An object that implements <see cref="IMainViewModel"/> representing the main view model.</returns>
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
        /// <summary>
        /// Creates a new instance of an object that implements the main layout settings view model interface.
        /// </summary>
        /// <returns>An object implementing <see cref="IMainLayoutSettingViewModel"/> representing the main layout settings view
        /// model.</returns>
        public static IMainLayoutSettingViewModel CreateMainLayoutSettingViewModel()
        {
            return new MainLayoutSettingViewModel();
        }

        #region LoansViewModels
        /// <summary>
        /// loader for addedite view model
        /// </summary>
        /// <returns></returns>
        public static IAddEditeLoanViewModel CreateAddEditeLoanViewModel()
        {
            IAddEditeLoanViewModel ViewModel = new AddEditeLoanViewModel();
            ViewModel.LoadBooksCommand.Execute(null);
            ViewModel.LoadClientsCommand.Execute(null);
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

        #endregion

        #region SettingsViewModels

        /// <summary>
        /// Creates a new instance of a view model for managing security settings.
        /// </summary>
        /// <returns>An object that implements <see cref="ISecuritySettingsViewModel"/> for interacting with security settings.</returns>
        public static ISecuritySettingsViewModel CreateSecuritySettingsViewModel()
        {
            return new SecuritySettingsViewModel();
        }

        /// <summary>
        /// Creates a new instance of a view model for loan settings.
        /// </summary>
        /// <returns>An object that implements <see cref="ILoanSettingsViewModel"/> representing the loan settings view model.</returns>
        public static ILoanSettingsViewModel CreateLoanSettingsViewModel()
        {
            return new LoanSettingsViewModel();
        }
        /// <summary>
        /// Creates a new instance of an object that implements the <see cref="ISettingsViewModel"/> interface.
        /// </summary>
        /// <returns>An <see cref="ISettingsViewModel"/> instance representing the application's settings view model.</returns>
        public static ISettingsViewModel CreateSettingsViewModel()
        {
            return new SettingsViewModel();
        }

        #endregion

        #region ReservedBooksViewModels

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
        /// <returns></returns>
        public static IAddEditeReserveBookViewModel CreateAddEditeReserveBookViewModel()
        {
            AddEditeReserveBookViewModel ViewModel = new AddEditeReserveBookViewModel();
            ViewModel.LoadBooksCommand.Execute(null);
            ViewModel.LoadClientsCommand.Execute(null);
            //ViewModel.SelectedReservedBook = reservedBook is null ? new ReservedBook() { ID = 0, BookId = 0, ClientId = 0 } : reservedBook;
            return ViewModel;
        }


        #endregion






        #region MessageBoxViewModel
        public static IMessageBoxViewModel CreateMessageBoxViewModel(IMessageBoxStore messageBoxStore, string title, string caption, string firstBtText = null, string secondBtTetxt = null, ICommand command = null)
        {
            return new MessageBoxViewModel(messageBoxStore, title, caption, firstBtText, secondBtTetxt, command);
        }
        #endregion

        #endregion

        #region Commands
        /// <summary>
        /// Creates a new command instance that can be used to close the application.
        /// </summary>
        /// <returns>An object implementing <see cref="ICloseAppCommand"/> that represents the close application command.</returns>
        public static ICloseAppCommand CreateCloseAppCommand()
        {
            return new CloseAppCommand();
        }
        /// <summary>
        /// Creates a new instance of an object that implements the ILoginCommand interface.
        /// </summary>
        /// <returns>An ILoginCommand instance that can be used to perform login operations.</returns>
        public static ILoginCommand CreateLoginCommand()
        {
            return new LoginCommand();
        }

        /// <summary>
        /// Creates a new instance of a command used to display and manage a login modal dialog.
        /// </summary>
        /// <returns>An object implementing <see cref="ILoginModalCommand"/> that can be used to control a login modal dialog.</returns>
        public static ILoginModalCommand CreateLoginModalCommand()
        {
            return new LoginModalCommand();
        }
        /// <summary>
        /// Creates a new command instance that closes a modal dialog.
        /// </summary>
        /// <returns>An object implementing <see cref="ICloseModalCommand"/> that can be used to close a modal dialog.</returns>
        public static ICloseModalCommand CreateCloseModalCommand()
        {
            return new CloseModalCommand();
        }
        #region NavigateCommands

        /// <summary>
        /// Creates a new command instance that navigates to the application's home screen.
        /// </summary>
        /// <returns>An object implementing <see cref="INavigateHomeScreenCommand"/> that can be executed to perform navigation
        /// to the home screen.</returns>
        public static INavigateHomeScreenCommand CreateNavigateHomeScreenCommand()
        {
            return new NavigateHomeScreenCommand();
        }

        /// <summary>
        /// Creates a new instance of a command that navigates to the client screen.
        /// </summary>
        /// <returns>An object implementing <see cref="INavigateClientScreenCommand"/> that can be used to initiate navigation to
        /// the client screen.</returns>
        public static INavigateClientScreenCommand CreateNavigateClientScreenCommand()
        {
            return new NavigateClientScreenCommand();
        }
        /// <summary>
        /// Creates a command that navigates to the books screen.
        /// </summary>
        /// <returns>An instance of <see cref="INavigateBooksCommand"/> that can be used to initiate navigation to the books
        /// screen.</returns>
        public static INavigateBooksCommand CreateNavigateBooksScreenCommand()
        {
            return new NavigateBooksCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to navigate loans within the application.
        /// </summary>
        /// <returns>An object that implements <see cref="INavigateLoansCommand"/> for navigating loans.</returns>
        public static INavigateLoansCommand CreateNavigateLoansCommand()
        {
            return new NavigateLoansCommand();
        }
        /// <summary>
        /// Creates a new command instance for navigating to the reserved books section.
        /// </summary>
        /// <returns>An object implementing <see cref="INavigateReservedBooksCommand"/> that can be used to initiate navigation
        /// to reserved books.</returns>
        public static INavigateReservedBooksCommand CreateNavigateReservedBooksCommand()
        {
            return new NavigateReservedBooksCommand();
        }
        /// <summary>
        /// Creates a command that navigates to the application's settings page.
        /// </summary>
        /// <returns>An instance of <see cref="INavigateToSettingsCommand"/> that can be executed to open the settings page.</returns>
        public static INavigateToSettingsCommand CreateNavigateToSettingsCommand()
        {
            return new NavigateToSettingsCommand();
        }

        #endregion

        #region DatabaseCommands
        /// <summary>
        /// Creates a new command instance for checking the status or integrity of a database.
        /// </summary>
        /// <returns>An object implementing <see cref="ICheckDatabaseCommand"/> that can be used to perform database checks.</returns>
        public static ICheckDatabaseCommand CreateCheckDatabaseCommand()
        {
            return new CheckDatabaseCommand();
        }
        #endregion

        #region LoansCommands
        /// <summary>
        /// Creates a new command instance for displaying the loan modal dialog.
        /// </summary>
        /// <returns>An <see cref="IShowLoanModalCommand"/> that can be used to show the loan modal dialog.</returns>
        public static IShowLoanModalCommand CreateShowLoanModalCommand()
        {
            return new ShowLoanModalCommand();
        }
        /// <summary>
        /// Creates a new instance of an object that implements the ILoadLoansCommand interface.
        /// </summary>
        /// <returns>An ILoadLoansCommand instance that can be used to load loan data.</returns>
        public static ILoadLoansCommand CreateLoadLoansCommand()
        {
            return new LoadLoansCommand();
        }
        /// <summary>
        /// Creates a new instance of a view model for displaying or editing loan information.
        /// </summary>
        /// <returns>An object that implements <see cref="IShowEditLoanViewModel"/> for managing the display and editing of loan
        /// details.</returns>
        public static IShowEditLoanViewModel CreateShowEditLoanViewModel()
        {
            return new ShowEditLoanViewModel();
        }
        /// <summary>
        /// Creates a new instance of a command that sorts a list of loans.
        /// </summary>
        /// <returns>An object implementing <see cref="ISortLoansListCommand"/> that can be used to sort loan lists.</returns>
        public static ISortLoansListCommand CreateSortLoansListCommand()
        {
            return new SortLoansListCommand();
        }

        /// <summary>
        /// Creates a new instance of a command used to process returned loans.
        /// </summary>
        /// <returns>An object implementing <see cref="IReturnedLoanCommand"/> that can be used to handle returned loan
        /// operations.</returns>
        public static IReturnedLoanCommand CreateReturnedLoanCommand()
        {
            return new ReturnedLoanCommand();
        }
        /// <summary>
        /// Creates a new instance of a command that reloads the loans list.
        /// </summary>
        /// <returns>An object implementing <see cref="IReloadLoansListCommand"/> that can be used to trigger a reload of the
        /// loans list.</returns>
        internal static IReloadLoansListCommand CreateReloadLoansListCommand()
        {
            return new ReloadLoansListCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to save loan data.
        /// </summary>
        /// <returns>An object that implements <see cref="ISaveLoanDataCommand"/> for saving loan data.</returns>
        public static ISaveLoanDataCommand CreateSaveLoanDataCommand()
        {
            return new SaveLoanDataCommand();
        }

        #endregion

        #region BooksCommands
        /// <summary>
        /// Creates a new instance of a command used to search for books.
        /// </summary>
        /// <returns>An object that implements <see cref="ISearchBookCommand"/> for performing book search operations.</returns>
        public static ISearchBookCommand CreateSearchBookCommand()
        {
            return new SearchBookCommand();
        }
        /// <summary>
        /// Creates a new command instance for retrieving order books grouped by state.
        /// </summary>
        /// <returns>An <see cref="IOrderBooksByStateCommand"/> that can be used to query order books by their state.</returns>
        public static IOrderBooksByStateCommand CreateOrderBooksCommand()
        {
            return new OrderBooksByStateCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to load books.
        /// </summary>
        /// <returns>An object that implements <see cref="ILoadBooksCommand"/> for loading books.</returns>
        public static ILoadBooksCommand CreateLoadBooksCommand()
        {
            return new LoadBooksCommand();
        }

        /// <summary>
        /// Creates a new command instance for searching book names.
        /// </summary>
        /// <returns>An object that implements <see cref="ISearchBookNameCommand"/> for performing book name search operations.</returns>
        public static ISearchBookNameCommand CreateSearchBookNameCommand()
        {
            return new SearchBookNameCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to order books by subject.
        /// </summary>
        /// <returns>An <see cref="IOrderBooksBySubjectCommand"/> instance that can be used to order books by subject.</returns>
        public static IOrderBooksBySubjectCommand CreateOrderBooksBySubjectCommand()
        {
            return new OrderBooksBySubjectCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to add a book to the collection.
        /// </summary>
        /// <returns>An object implementing <see cref="IAddNewBookCommand"/> that can be used to add a new book.</returns>
        public static IAddNewBookCommand CreateAddNewBookCommand()
        {
            return new AddNewBookCommand();
        }
        /// <summary>
        /// <see langword="static"/> method that creates a new instance of a command used to edit book information.
        /// </summary>
        /// <returns></returns>
        public static IEditBookCommand CreateEditBookCommand()
        {
            return new EditBookCommand();
        }
        /// <summary>
        /// <see langword="static"/> method that creates a new instance of a command used to delete a book from the collection.
        /// </summary>
        /// <returns></returns>
        public static IDeleteBookCommand CreateDeleteBookCommand()
        {
            return new DeleteBookCommand();
        }
        /// <summary>
        /// method that creates a new instance of a command used to reload the list of books.
        /// </summary>
        /// <returns></returns>
        public static IReloadBooksCommand CreateReloadBooksCommand()
        {
            return new ReloadBooksCommand();
        }
        #endregion

        #region ReservedBooksCommands
        /// <summary>
        /// Creates a new command instance for saving reservation data.
        /// </summary>
        /// <returns>An object that implements <see cref="ISaveReservationDataCommand"/> for saving reservation data.</returns>
        public static ISaveReservationDataCommand CreateSaveReservationDataCommand()
        {
            return new SaveReservationDataCommand();
        }
        /// <summary>
        /// Creates a new command instance for loading reserved books.
        /// </summary>
        /// <returns>An object that implements <see cref="ILoadReservedBooksCommand"/> for retrieving reserved book data.</returns>
        public static ILoadReservedBooksCommand CreateLoadReservedBooksCommand()
        {
            return new LoadReservedBooksCommand();
        }
        /// <summary>
        /// Creates a new instance of a command for editing reserved books.
        /// </summary>
        /// <returns>An object that implements <see cref="IEditeReservBookCommand"/> for editing reserved book information.</returns>
        public static IEditeReservBookCommand CreateEditeReservBookCommand()
        {
            return new EditeReservBookCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to add a reserved book.
        /// </summary>
        /// <returns>An object that implements <see cref="IAddNewReservBookCommand"/> for adding a reserved book.</returns>
        public static IAddNewReservBookCommand CreateAddNewReservBookCommand()
        {
            return new AddNewReservBookCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to remove a reserved book.
        /// </summary>
        /// <returns>An object implementing <see cref="IRemoveReservBookCommand"/> that can be used to remove a reserved book.</returns>
        public static IRemoveReservBookCommand CreateRemoveReservBookCommand()
        {
            return new RemoveReservBookCommand();
        }
        /// <summary>
        /// Creates a new instance of a command that resets the reservation state of a book.
        /// </summary>
        /// <returns>An object implementing <see cref="IResetReservBookCommand"/> that can be used to reset a book's reservation
        /// status.</returns>
        public static IResetReservBookCommand CreateResetReservBookCommand()
        {
            return new ResetReservBookCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to search for book names within reserved books.
        /// </summary>
        /// <returns>An object implementing <see cref="ISearchBookNameInReservedBookCommand"/> that can be used to perform book
        /// name searches in reserved books.</returns>
        public static ISearchBookNameInReservedBookCommand CreateSearchBookNameInReservedBookCommand()
        {
            return new SearchBookNameInReservedBookCommand();
        }
        #endregion

        #region SettingsCommands
        public static INavigateLoanSettingsCommand CreateNavigateLoanSettingsCommand()
        {
            return new NavigateLoanSettingsCommand();
        }
        public static INavigateLayoutSettingCommand CreateNavigateLayoutSettingCommand()
        {
            return new NavigateLayoutSettingCommand();
        }
        public static INavigateSecuritySettingsCommand CreateNavigateSecuritySettingsCommand()
        {
            return new NavigateSecuritySettingsCommand();
        }
        public static IChangeLoginPasswordCommand CreateChangeLoginPasswordCommand()
        {
            return new ChangeLoginPasswordCommand();
        }
        #endregion

        #region ClientsCommands
        /// <summary>
        ///  method that creates a new instance of a command used to reload the list of clients.
        /// </summary>
        /// <returns></returns>
        public static IReloadClientsCommand CreateReloadClientsCommand()
        {
            return new ReloadClientsCommand();
        }
        /// <summary>
        ///method that creates a new instance of a command used to delete a client from the collection.
        /// </summary>
        /// <returns></returns>
        public static IDeleteClientCommand CreateDeleteClientCommand()
        {
            return new DeleteClientCommand();
        }
        /// <summary>
        /// method that creates a new instance of a command used to add a client to the collection.
        /// </summary>
        /// <returns></returns>
        public static IAddNewClientCommand CreateAddNewClientCommand()
        {
            return new AddNewClientCommand();
        }
        /// <summary>
        ///  method that creates a new instance of a command used to order clients by name.
        /// </summary>
        /// <returns></returns>
        public static IOrderClientsCommand CreateOrderClientsCommand()
        {
            return new OrderClientsCommand();
        }
        /// <summary>
        /// method that creates a new instance of a command used to edit client information.
        /// </summary>
        /// <returns></returns>
        public static IEditClientCommand CreateEditClientCommand()
        {
            return new EditClientCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to load client data.
        /// </summary>
        /// <returns>An object that implements <see cref="ILoadClientsCommand"/> for loading clients.</returns>
        public static ILoadClientsCommand CreateLoadClientsCommand()
        {
            return new LoadClientsCommand();
        }
        /// <summary>
        /// Creates a new instance of a command used to search for client names.
        /// </summary>
        /// <returns>An object that implements <see cref="ISearchClientNameCommand"/> for performing client name searches.</returns>
        public static ISearchClientNameCommand CreateSearchClientNameCommand()
        {
            return new SearchClientNameCommand();
        }
        #endregion
        #endregion

        #region Stores
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the navigation store interface.
        /// </summary>
        /// <returns></returns>
        public static INavigationStore CreateNavigationStore()
        {
            return new NavigationStore();
        }

        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the books store interface.
        /// </summary>
        /// <returns></returns>
        public static IBooksStore CreateBooksStore()
        {
            return new BooksStore();
        }

        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the clients store interface.
        /// </summary>
        /// <returns></returns>
        public static IClientsStore CreateClientsStore()
        {
            return new ClientsStore();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the loans store interface.
        /// </summary>
        /// <returns></returns>
        public static ILoansStore CreateLoansStore()
        {
            return new LoansStore();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the login store interface.
        /// </summary>
        /// <returns></returns>
        public static IModalNavigationStore CreateModalNavigationStore()
        {
            return new ModalNavigationStore();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the message box store interface.
        /// </summary>
        /// <returns></returns>
        public static IMessageBoxStore CreateMessageBoxStore()
        {
            return new MessageBoxStore();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the reserved books store interface.
        /// </summary>
        /// <returns></returns>
        public static IReservedBooksStore CreateReservedBooksStore()
        {
            return new ReservedBooksStore();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the settings navigation store interface.
        /// </summary>
        /// <returns></returns>
        public static ISettingNavigationStore CreateSettingNavigationStore()
        {
            return new SettingNavigationStore();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the settings store interface.
        /// </summary>
        /// <returns></returns>
        public static ISettingsStore CreateSettingsStore()
        {
            return new SettingsStore();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the time store interface.
        /// </summary>
        /// <returns></returns>
        public static ITimeStore CreateTimeStore()
        {
            return new TimeStore();
        }
        #endregion

        #region Repositories
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the database context factory interface.
        /// </summary>
        /// <returns></returns>
        public static IDbContextFactory CreateDbContextFactory()
        {
            return new DbContextFactory();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the books repository interface.
        /// </summary>
        /// <returns></returns>
        public static IBooksRepository CreateBooksRepository()
        {
            return new BooksRepository();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the clients repository interface.
        /// </summary>
        /// <returns></returns>
        public static IClientsRepository CreateClientsRepository()
        {
            return new ClientsRepository();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the loans repository interface.
        /// </summary>
        /// <returns></returns>
        public static ILoanRepository CreateLoanRepository()
        {
            return new LoanRepository();
        }
        /// <summary>
        /// <see langword="fixed"/> method that creates a new instance of an object that implements the reserved books repository interface. 
        /// </summary>
        /// <returns></returns>
        public static IReservedBooksRepository CreateReservedBooksRepository()
        {
            return new ReservedBooksRepository();
        }

        #endregion
    }
}
