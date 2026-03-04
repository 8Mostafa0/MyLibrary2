using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BooksCommands;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using MyLibrary.ViewModel.Commands.LoansCommands;
using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public interface IAddEditeReserveBookViewModel : IViewModelBase
    {
        string BookName { get; set; }
        IEnumerable<Book> Books { get; }
        int BookSubject { get; set; }
        string ClientName { get; set; }
        IEnumerable<Client> Clients { get; }
        ICloseModalCommand CloseModalCommand { get; }
        ILoadBooksCommand LoadBooksCommand { get; }
        ILoadClientsCommand LoadClientsCommand { get; }
        IOrderBooksBySubjectCommand OrderBooksBySubjectCommand { get; }
        IOrderBooksBySubjectCommand OrderBooksCommand { get; }
        ISaveReservationDataCommand SaveReservedBookDataCommand { get; }
        ISearchBookNameInReservedBookCommand SearchBookNameCommand { get; }
        ISearchClientNameCommand SearchClientNameCommand { get; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        ReservedBook SelectedReservedBook { get; set; }
        string TitleOfLoanScreen { get; set; }
    }
}