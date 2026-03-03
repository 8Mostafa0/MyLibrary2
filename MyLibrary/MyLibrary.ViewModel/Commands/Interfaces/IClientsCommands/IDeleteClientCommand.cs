using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public interface IDeleteClientCommand : ICommand
    {
        void Execute(object parameter);
    }
}