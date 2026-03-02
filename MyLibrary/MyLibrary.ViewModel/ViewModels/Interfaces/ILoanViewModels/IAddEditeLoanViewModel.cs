using MyLibrary.Model.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public interface IAddEditeLoanViewModel : IViewModelBase
    {
        IEnumerable<Book> Books { get; }
        string BookSearch { get; set; }
        int BooksSortOrder { get; set; }
        IEnumerable<Client> Clients { get; }
        string ClientSearch { get; set; }
        ICommand CloseModalCommand { get; }
        IViewModelBase CurrentModelViewModel { get; }
        ICommand LoadBooksCommand { get; }
        ICommand LoadClientsCommand { get; }
        ICommand OrderBooksBySubjectCommand { get; }
        DateTime ReturnDate { get; set; }
        ICommand SaveLoanDataCommand { get; }
        ICommand SearchBookNameCommand { get; }
        ICommand SearchClientNameCommand { get; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        Loan SelectedLoan { get; set; }
        string TitleOfLoanScreen { get; set; }

    }
}