using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoginCommands;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.Commands.SettingsCommands;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface INavigationBarViewModel
    {
        INavigateClientScreenCommand ClientsCreenCommand { get; }
        ICloseAppCommand CloseAppCommand { get; }
        ICommand DatabaseCommand { get; }
        INavigateBooksCommand NavigateBooksCommand { get; }
        INavigateHomeScreenCommand NavigateHomeCommand { get; }
        AsyncRelayCommand NavigateLoansCommand { get; }
        INavigateReservedBooksCommand NavigateReservedBooksCommand { get; }
        INavigateToSettingsCommand NavigateToSettingsCommand { get; }
        ICommand OpenModalCommand { get; }
    }
}