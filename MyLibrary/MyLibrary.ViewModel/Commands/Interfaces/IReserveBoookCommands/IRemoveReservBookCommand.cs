using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public interface IRemoveReservBookCommand : ICommand
    {
        new void Execute(object parameter);
    }
}