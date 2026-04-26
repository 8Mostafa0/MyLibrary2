using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public interface IDeleteClientCommand : ICommand
    {
        new void Execute(object parameter);
    }
}