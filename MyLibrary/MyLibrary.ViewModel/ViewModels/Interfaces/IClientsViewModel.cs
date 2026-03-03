using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.ClientsCommands;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IClientsViewModel : IViewModelBase
    {
        IAddNewClientCommand AddNewClientCommand { get; }
        IEnumerable<Client> Clients { get; }
        IViewModelBase CurrentMessageBox { get; }
        IDeleteClientCommand DeleteClientCommand { get; }
        ICommand EditClientCommand { get; }
        string FirstName { get; set; }
        bool IsMessageBoxOpen { get; }
        string LastName { get; set; }
        ILoadClientsCommand LoadClientsCommand { get; }
        IOrderClientsCommand OrderClientsCommand { get; }
        IReloadClientsCommand ReloadClientsCommand { get; }
        Client SelectedClient { get; set; }
        string SortOrder { get; set; }
        int Tier { get; set; }
        void UpdateClients();
    }
}