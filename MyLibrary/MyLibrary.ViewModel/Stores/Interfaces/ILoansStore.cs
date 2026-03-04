using MyLibrary.Model.Models;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public interface ILoansStore
    {
        IEnumerable<LoanViewModel> Loans { get; }

        event Action<Loan> LoanIsAdded;
        event Action<Loan> LoanIsReturned;
        event Action<Loan> LoanIsUpdated;
        event Action LoansUpdated;

        LoanViewModel SelectedLoan { get; set; }
        string BookName { get; set; }
        string ClientName { get; set; }
        int SortIndex { get; set; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        Task AddLoan(Loan loan);
        Task GetAllLoans(string customSql = "");
        Task Load();
        Task LoanReturned(Loan loan);
        Task LoanUpdated(Loan loan);
        Task UpdateLoan(Loan loan);
    }
}