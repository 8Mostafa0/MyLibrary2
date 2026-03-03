using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public interface IRemoveReservBookCommand : ICommand
    {
        void Execute(object parameter);
    }
}