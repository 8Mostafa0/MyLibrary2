using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public interface IDeleteBookCommand : ICommand
    {
        void Execute(object parameter);
    }
}