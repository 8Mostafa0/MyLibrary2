using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public interface ICloseAppCommand : ICommand
    {
        new void Execute(object parameter);
    }
}