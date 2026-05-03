using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BaseCommands;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.LoanViewModels
{
    public interface IAddEditeLoanViewModel : IViewModelBase
    {
        ObservableCollection<Book> Books { get; }
        string BookSearch { get; set; }
        int BooksSortOrder { get; set; }
        ObservableCollection<Client> Clients { get; }
        string ClientSearch { get; set; }
        RelayCommand CloseModalCommand { get; }
        IViewModelBase CurrentModelViewModel { get; }
        AsyncRelayCommand OrderBooksBySubjectCommand { get; }
        DateTime ReturnDate { get; set; }
        AsyncRelayCommand SaveLoanDataCommand { get; }
        AsyncRelayCommand SearchBookNameCommand { get; }
        AsyncRelayCommand SearchClientNameCommand { get; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        Loan SelectedLoan { get; set; }
        string TitleOfLoanScreen { get; set; }
        Task RefreshPage();

    }
}