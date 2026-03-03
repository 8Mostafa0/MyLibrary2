using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public interface ICloseAppCommand : ICommand
    {
        void Execute(object parameter);
    }
}