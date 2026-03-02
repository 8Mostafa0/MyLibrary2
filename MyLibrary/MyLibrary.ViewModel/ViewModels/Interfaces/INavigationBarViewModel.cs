using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface INavigationBarViewModel
    {
        INavigateClientScreenCommand ClientsCreenCommand { get; }
        ICommand CloseAppCommand { get; }
        ICommand DatabaseCommand { get; }
        ICommand NavigateBooksCommand { get; }
        INavigateHomeScreenCommand NavigateHomeCommand { get; }
        ICommand NavigateLoansCommand { get; }
        ICommand NavigateReservedBooksCommand { get; }
        ICommand NavigateToSettingsCommand { get; }
        ICommand OpenModalCommand { get; }
    }
}