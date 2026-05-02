using System;

namespace MyLibrary.ViewModel.Commands
{
    public interface ICommandBase
    {
        event EventHandler CanExecuteChanged;

        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}