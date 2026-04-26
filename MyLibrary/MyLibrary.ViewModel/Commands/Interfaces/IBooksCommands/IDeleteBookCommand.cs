using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public interface IDeleteBookCommand : ICommand
    {
        new void Execute(object parameter);
    }
}