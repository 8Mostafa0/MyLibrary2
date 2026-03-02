using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public interface IAddEditeReserveBookViewModel : IViewModelBase
    {
        string BookName { get; set; }
        IEnumerable<Book> Books { get; }
        int BookSubject { get; set; }
        string ClientName { get; set; }
        IEnumerable<Client> Clients { get; }
        ICommand CloseModalCommand { get; }
        ICommand LoadBooksCommand { get; }
        ICommand LoadClientsCommand { get; }
        ICommand OrderBooksBySubjectCommand { get; }
        ICommand OrderBooksCommand { get; }
        ICommand SaveReservedBookDataCommand { get; }
        ICommand SearchBookNameCommand { get; }
        ICommand SearchClientNameCommand { get; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        ReservedBook SelectedReservedBook { get; set; }
        string TitleOfLoanScreen { get; set; }
    }
}