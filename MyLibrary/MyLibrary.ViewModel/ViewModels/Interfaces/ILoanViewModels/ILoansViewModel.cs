using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BaseCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public interface ILoansViewModel : IViewModelBase
    {
        string BookName { get; set; }
        IViewModelBase CurrentModalViewModel { get; }
        bool IsModalOpen { get; }
        ILoadLoansCommand LoadLoansCommand { get; }
        IEnumerable<Loan> Loans { get; }
        IReloadLoansListCommand ReloadLoansListCommand { get; }
        IReturnedLoanCommand ReturnedLoanCommand { get; }
        ISearchBookCommand SearchBookCommand { get; }
        Loan SelectedLoan { get; set; }
        AsyncRelayCommand ShowAddLoanModalCommand { get; }
        AsyncRelayCommand ShowEditLoanViewModel { get; }
        int SortIndex { get; set; }
        ISortLoansListCommand SortLoansListCommand { get; }
    }
}