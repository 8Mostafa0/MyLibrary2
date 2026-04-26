using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public interface IReturnedLoanCommand : ICommand
    {
        new void Execute(object parameter);
    }
}