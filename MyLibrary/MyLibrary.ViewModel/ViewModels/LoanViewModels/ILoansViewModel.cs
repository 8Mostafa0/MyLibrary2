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
        ICommand LoadLoansCommand { get; }
        IEnumerable<LoanViewModel> Loans { get; }
        ICommand OrderBooksCommand { get; }
        ICommand ReloadLoansListCommand { get; }
        ICommand ReturnedLoanCommand { get; }
        ICommand SearchBookCommand { get; }
        LoanViewModel SelectedLoan { get; set; }
        ICommand ShowAddLoanModalCommand { get; }
        ICommand ShowEditLoanViewModel { get; }
        int SortIndex { get; set; }
        ICommand SortLoansListCommand { get; }
        void UpdateLoans();
    }
}