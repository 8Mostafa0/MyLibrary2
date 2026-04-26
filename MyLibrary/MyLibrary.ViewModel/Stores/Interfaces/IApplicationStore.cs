using MyLibrary.Model.Models;

namespace MyLibrary.ViewModel.Stores
{
    public interface IApplicationStore
    {
        bool IsDatabaseConnected { get; set; }
        bool IsLoading { get; set; }
        Book SelectedBook { get; set; }
        Client SelectedClient { get; set; }
        Loan SelectedLoan { get; set; }
        ReservedBook SelectedReservedBook { get; set; }
    }
}