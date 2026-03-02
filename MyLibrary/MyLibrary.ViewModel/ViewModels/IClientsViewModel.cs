using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IClientsViewModel : IViewModelBase
    {
        ICommand AddNewClientCommand { get; }
        IEnumerable<Client> Clients { get; }
        IViewModelBase CurrentMessageBox { get; }
        ICommand DeleteClientCommand { get; }
        ICommand EditClientCommand { get; }
        string FirstName { get; set; }
        bool IsMessageBoxOpen { get; }
        string LastName { get; set; }
        ICommand LoadClientsCommand { get; }
        ICommand OrderClientsCommand { get; }
        ICommand ReloadClientsCommand { get; }
        Client SelectedClient { get; set; }
        string SortOrder { get; set; }
        int Tier { get; set; }

        static abstract ClientsViewModel LoadViewModel(ClientsStore clientStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository, MessageBoxStore messageBoxStore);
        void UpdateClients();
    }
}