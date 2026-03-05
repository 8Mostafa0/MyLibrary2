using MyLibrary.ViewModel.Commands.BookApiCommands;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IBookApiViewModel
    {
        string Author { get; }
        string Description { get; }
        string Name { get; }
        INextBookCommand NextBookCommand { get; }
        IpreviousBookCommand PreviousBookCommand { get; }
    }
}