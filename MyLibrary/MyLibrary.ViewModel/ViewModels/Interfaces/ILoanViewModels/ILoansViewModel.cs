using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public interface ILoansViewModel : IViewModelBase
    {
        string BookName { get; set; }
        IViewModelBase CurrentModalViewModel { get; }
        bool IsModalOpen { get; }
        ILoadLoansCommand LoadLoansCommand { get; }
        IEnumerable<LoanViewModel> Loans { get; }
        ICommand OrderBooksCommand { get; }
        ICommand ReloadLoansListCommand { get; }
        IReturnedLoanCommand ReturnedLoanCommand { get; }
        ICommand SearchBookCommand { get; }
        LoanViewModel SelectedLoan { get; set; }
        IShowLoanModalCommand ShowAddLoanModalCommand { get; }
        IShowEditLoanViewModel ShowEditLoanViewModel { get; }
        int SortIndex { get; set; }
        ISortLoansListCommand SortLoansListCommand { get; }
        void UpdateLoans();
    }
}