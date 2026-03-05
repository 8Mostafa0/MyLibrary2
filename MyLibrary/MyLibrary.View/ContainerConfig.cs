using Autofac;
using ModalControl;
using MyLibrary.Model.DbContexts;
using MyLibrary.Model.Repositories;
using MyLibrary.View;
using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Commands.BookApiCommands;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Commands.LoginCommands;
using MyLibrary.ViewModel.Commands.MessageBoxCommands;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Commands.SettingsCommands;
using MyLibrary.ViewModel.Services.BookApi;
using MyLibrary.ViewModel.Servicies;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;
using MyLibrary.ViewModel.ViewModels.MessageBoxViewModel;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;
using MyLibrary.ViewModel.ViewModels.SettingsViewModels;
using System.Reflection;
namespace MyLibrary.ViewModel
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();


            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();


            #region ViewModels
            builder.RegisterType<ClientsStore>().As<IClientsStore>().SingleInstance();

            builder.RegisterType<LoansViewModel>().As<ILoansViewModel>().SingleInstance();
            builder.RegisterType<AddEditeLoanViewModel>().As<IAddEditeLoanViewModel>().SingleInstance();

            builder.RegisterType<MessageBoxViewModel>().As<IMessageBoxViewModel>().SingleInstance();

            builder.RegisterType<AddEditeReserveBookViewModel>().As<IAddEditeReserveBookViewModel>().SingleInstance();
            builder.RegisterType<ReservedBooksViewModel>().As<IReservedBooksViewModel>().SingleInstance();

            builder.RegisterType<LoanSettingsViewModel>().As<ILoanSettingsViewModel>().SingleInstance();
            builder.RegisterType<MainLayoutSettingViewModel>().As<IMainLayoutSettingViewModel>().SingleInstance();
            builder.RegisterType<SecuritySettingsViewModel>().As<ISecuritySettingsViewModel>().SingleInstance();
            builder.RegisterType<SettingsViewModel>().As<ISettingsViewModel>().SingleInstance();

            builder.RegisterType<BooksViewModel>().As<IBooksViewModel>().SingleInstance();
            builder.RegisterType<ClientsViewModel>().As<IClientsViewModel>().SingleInstance();
            builder.RegisterType<HomeViewModel>().As<IHomeViewModel>().SingleInstance();
            builder.RegisterType<LayoutViewModel>().As<ILayoutViewModel>().SingleInstance();
            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>().SingleInstance();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>().SingleInstance();
            builder.RegisterType<NavigationBarViewModel>().As<INavigationBarViewModel>().SingleInstance();
            builder.RegisterType<StatusBarViewModel>().As<IStatusBarViewModel>().SingleInstance();
            builder.RegisterType<BookApiViewModel>().As<IBookApiViewModel>().SingleInstance();

            #endregion

            #region Commands

            builder.RegisterType<AddNewBookCommand>().As<IAddNewBookCommand>().SingleInstance();
            builder.RegisterType<DeleteBookCommand>().As<IDeleteBookCommand>().SingleInstance();
            builder.RegisterType<EditBookCommand>().As<IEditBookCommand>().SingleInstance();
            builder.RegisterType<LoadBooksCommand>().As<ILoadBooksCommand>().SingleInstance();
            builder.RegisterType<NavigateBooksCommand>().As<INavigateBooksCommand>().SingleInstance();
            builder.RegisterType<OrderBooksBySubjectCommand>().As<IOrderBooksBySubjectCommand>().SingleInstance();
            builder.RegisterType<OrderBooksByStateCommand>().As<IOrderBooksByStateCommand>().SingleInstance();
            builder.RegisterType<ReloadBooksCommand>().As<IReloadBooksCommand>().SingleInstance();
            builder.RegisterType<SearchBookNameCommand>().As<ISearchBookNameCommand>().SingleInstance();

            builder.RegisterType<AddNewClientCommand>().As<IAddNewClientCommand>().SingleInstance();
            builder.RegisterType<DeleteClientCommand>().As<IDeleteClientCommand>().SingleInstance();
            builder.RegisterType<EditClientCommand>().As<IEditClientCommand>().SingleInstance();
            builder.RegisterType<LoadClientsCommand>().As<ILoadClientsCommand>().SingleInstance();
            builder.RegisterType<NavigateClientScreenCommand>().As<INavigateClientScreenCommand>().SingleInstance();
            builder.RegisterType<OrderClientsCommand>().As<IOrderClientsCommand>().SingleInstance();
            builder.RegisterType<ReloadClientsCommand>().As<IReloadClientsCommand>().SingleInstance();
            builder.RegisterType<SearchClientNameCommand>().As<ISearchClientNameCommand>().SingleInstance();


            builder.RegisterType<CloseModalCommand>().As<ICloseModalCommand>().SingleInstance();
            builder.RegisterType<LoadLoansCommand>().As<ILoadLoansCommand>().SingleInstance();
            builder.RegisterType<NavigateLoansCommand>().As<INavigateLoansCommand>().SingleInstance();
            builder.RegisterType<ReloadLoansListCommand>().As<IReloadLoansListCommand>().SingleInstance();
            builder.RegisterType<ReturnedLoanCommand>().As<IReturnedLoanCommand>().SingleInstance();
            builder.RegisterType<SaveLoanDataCommand>().As<ISaveLoanDataCommand>().SingleInstance();
            builder.RegisterType<SearchBookCommand>().As<ISearchBookCommand>().SingleInstance();
            builder.RegisterType<ShowEditLoanViewModel>().As<IShowEditLoanViewModel>().SingleInstance();
            builder.RegisterType<ShowLoanModalCommand>().As<IShowLoanModalCommand>().SingleInstance();
            builder.RegisterType<SortLoansListCommand>().As<ISortLoansListCommand>().SingleInstance();

            builder.RegisterType<CloseAppCommand>().As<ICloseAppCommand>().SingleInstance();
            builder.RegisterType<LoginCommand>().As<ILoginCommand>().SingleInstance();

            builder.RegisterType<CloseMessageBox>().As<ICloseMessageBox>().SingleInstance();
            builder.RegisterType<MessageBoxConfrimCommand>().As<IMessageBoxConfrimCommand>().SingleInstance();

            builder.RegisterType<AddNewReservBookCommand>().As<IAddNewReservBookCommand>().SingleInstance();
            builder.RegisterType<EditeReservBookCommand>().As<IEditeReservBookCommand>().SingleInstance();
            builder.RegisterType<LoadReservedBooksCommand>().As<ILoadReservedBooksCommand>().SingleInstance();
            builder.RegisterType<NavigateReservedBooksCommand>().As<INavigateReservedBooksCommand>().SingleInstance();
            builder.RegisterType<RemoveReservBookCommand>().As<IRemoveReservBookCommand>().SingleInstance();
            builder.RegisterType<ResetReservBookCommand>().As<IResetReservBookCommand>().SingleInstance();
            builder.RegisterType<SaveReservationDataCommand>().As<ISaveReservationDataCommand>().SingleInstance();
            builder.RegisterType<SearchBookNameInReservedBookCommand>().As<ISearchBookNameInReservedBookCommand>().SingleInstance();

            builder.RegisterType<ChangeLoginPasswordCommand>().As<IChangeLoginPasswordCommand>().SingleInstance();
            builder.RegisterType<NavigateLayoutSettingCommand>().As<INavigateLayoutSettingCommand>().SingleInstance();
            builder.RegisterType<NavigateLoanSettingsCommand>().As<INavigateLoanSettingsCommand>().SingleInstance();
            builder.RegisterType<NavigateSecuritySettingsCommand>().As<INavigateSecuritySettingsCommand>().SingleInstance();
            builder.RegisterType<NavigateToSettingsCommand>().As<INavigateToSettingsCommand>().SingleInstance();


            builder.RegisterType<CheckDatabaseCommand>().As<ICheckDatabaseCommand>().SingleInstance();
            builder.RegisterType<LoginModalCommand>().As<ILoginModalCommand>().SingleInstance();
            builder.RegisterType<NavigateHomeScreenCommand>().As<INavigateHomeScreenCommand>().SingleInstance();

            builder.RegisterType<NextBookCommand>().As<INextBookCommand>().SingleInstance();
            builder.RegisterType<previousBookCommand>().As<IpreviousBookCommand>().SingleInstance();

            #endregion

            #region Stores
            builder.RegisterType<NavigationStore>().As<INavigationStore>();
            builder.RegisterType<BooksStore>().As<IBooksStore>().SingleInstance();
            builder.RegisterType<LoansStore>().As<ILoansStore>().SingleInstance();
            builder.RegisterType<SettingsStore>().As<ISettingsStore>().SingleInstance();
            builder.RegisterType<ClientsStore>().As<IClientsStore>().SingleInstance();
            builder.RegisterType<MessageBoxStore>().As<IMessageBoxStore>().SingleInstance();
            builder.RegisterType<ModalNavigationStore>().As<IModalNavigationStore>().SingleInstance();
            builder.RegisterType<NavigationStore>().As<INavigationStore>().SingleInstance();
            builder.RegisterType<ReservedBooksStore>().As<IReservedBooksStore>().SingleInstance();
            builder.RegisterType<SettingNavigationStore>().As<ISettingNavigationStore>().SingleInstance();
            builder.RegisterType<SettingsStore>().As<ISettingsStore>().SingleInstance();
            builder.RegisterType<TimeStore>().As<ITimeStore>().SingleInstance();
            builder.RegisterType<LoginStore>().As<ILoginStore>().SingleInstance();
            builder.RegisterType<BookApiStore>().As<IBookApiStore>().SingleInstance();
            #endregion

            #region Services
            builder.RegisterType<BookApi>().As<IBookApi>().SingleInstance();

            #endregion

            #region Repositorys
            builder.RegisterType<DbContextFactory>().As<IDbContextFactory>().SingleInstance();
            builder.RegisterType<ClientsRepository>().As<IClientsRepository>().SingleInstance();
            builder.RegisterType<LoanRepository>().As<ILoanRepository>().SingleInstance();
            builder.RegisterType<BooksRepository>().As<IBooksRepository>().SingleInstance();
            builder.RegisterType<ReservedBooksRepository>().As<IReservedBooksRepository>().SingleInstance();
            #endregion

            #region Logger
            builder.RegisterType<LoggerService>().As<ILoggerService>().SingleInstance();
            #endregion

            #region Modal
            builder.RegisterType<Modal>().As<IModal>().SingleInstance();
            #endregion
            return builder.Build();
        }
    }
}
