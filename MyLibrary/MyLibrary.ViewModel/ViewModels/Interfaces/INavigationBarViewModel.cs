namespace MyLibrary.ViewModel.ViewModels.Interfaces
{
    public interface INavigationBarViewModel
    {
        INavigateClientScreenCommand ClientsCreenCommand { get; }
        ICloseAppCommand CloseAppCommand { get; }
        ICommand DatabaseCommand { get; }
        INavigateBooksCommand NavigateBooksCommand { get; }
        INavigateHomeScreenCommand NavigateHomeCommand { get; }
        INavigateLoansCommand NavigateLoansCommand { get; }
        INavigateReservedBooksCommand NavigateReservedBooksCommand { get; }
        INavigateToSettingsCommand NavigateToSettingsCommand { get; }
        IOpenModalCommand OpenModalCommand { get; }
        virtual void Dispose() { }
    }
}
