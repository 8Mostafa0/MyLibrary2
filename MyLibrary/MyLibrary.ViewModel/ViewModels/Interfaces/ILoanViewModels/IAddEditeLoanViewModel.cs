using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using System;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public interface IAddEditeLoanViewModel : IViewModelBase
    {
        IEnumerable<Book> Books { get; }
        string BookSearch { get; set; }
        int BooksSortOrder { get; set; }
        IEnumerable<Client> Clients { get; }
        string ClientSearch { get; set; }
        ICloseModalCommand CloseModalCommand { get; }
        IViewModelBase CurrentModelViewModel { get; }
        ILoadBooksCommand LoadBooksCommand { get; }
        ILoadClientsCommand LoadClientsCommand { get; }
        IOrderBooksBySubjectCommand OrderBooksBySubjectCommand { get; }
        DateTime ReturnDate { get; set; }
        ISaveLoanDataCommand SaveLoanDataCommand { get; }
        ISearchBookNameCommand SearchBookNameCommand { get; }
        ISearchClientNameCommand SearchClientNameCommand { get; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        Loan SelectedLoan { get; set; }
        string TitleOfLoanScreen { get; set; }

    }
}