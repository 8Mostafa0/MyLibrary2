using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BaseCommands;
using System.Collections.ObjectModel;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public interface ILoansViewModel : IViewModelBase
    {
        string BookName { get; set; }
        IViewModelBase CurrentModalViewModel { get; }
        bool IsModalOpen { get; }
        ObservableCollection<Loan> Loans { get; }
        AsyncRelayCommand ReloadLoansListCommand { get; }
        AsyncRelayCommand ReturnedLoanCommand { get; }
        AsyncRelayCommand SearchBookCommand { get; }
        Loan SelectedLoan { get; set; }
        AsyncRelayCommand ShowAddLoanViewModalCommand { get; }
        AsyncRelayCommand ShowEditLoanViewModelCommand { get; }
        int SortIndex { get; set; }
        AsyncRelayCommand SortLoansListCommand { get; }
    }
}