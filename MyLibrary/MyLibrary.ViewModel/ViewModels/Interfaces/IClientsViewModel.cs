using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.Interfaces
{
    public interface IClientsViewModel : IViewModelBase
    {
        IAddNewClientCommand AddNewClientCommand { get; }
        IEnumerable<IClient> Clients { get; }
        IDeleteClientCommand DeleteClientCommand { get; }
        IEditClientCommand EditClientCommand { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        ILoadClientsCommand LoadClientsCommand { get; }
        IOrderClientsCommand OrderClientsCommand { get; }
        IReloadClientsCommand ReloadClientsCommand { get; }
        IClient SelectedClient { get; set; }
        string SortOrder { get; set; }
        int Tier { get; set; }

        static abstract IClientsViewModel LoadViewModel(IClientsStore clientStore, ILoanRepository loanRepository, IReservedBooksRepository reservedBooksRepository);
        void UpdateClients();
    }
}
