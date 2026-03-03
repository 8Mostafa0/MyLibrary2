using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public interface IReturnedLoanCommand : ICommand
    {
        void Execute(object parameter);
    }
}