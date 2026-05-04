using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BaseCommands;
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
        RelayCommand CloseModalCommand { get; }
        AsyncRelayCommand OrderBooksBySubjectCommand { get; }
        AsyncRelayCommand SaveReservedBookDataCommand { get; }
        AsyncRelayCommand SearchBookNameCommand { get; }
        AsyncRelayCommand SearchClientNameCommand { get; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        ReservedBook SelectedReservedBook { get; set; }
        string TitleOfLoanScreen { get; set; }
    }
}