using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BaseCommands;
using System.Collections.ObjectModel;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IClientsViewModel : IViewModelBase
    {
        AsyncRelayCommand AddNewClientCommand { get; }
        ObservableCollection<Client> _clients { get; }
        IViewModelBase CurrentMessageBox { get; }
        AsyncRelayCommand DeleteClientCommand { get; }
        AsyncRelayCommand EditClientCommand { get; }
        string FirstName { get; set; }
        bool IsMessageBoxOpen { get; }
        string LastName { get; set; }
        AsyncRelayCommand<Client> LoadClientsCommand { get; }
        AsyncRelayCommand<Client> OrderClientsCommand { get; }
        AsyncRelayCommand ReloadClientsCommand { get; }
        Client SelectedClient { get; set; }
        string SortOrder { get; set; }
        int Tier { get; set; }
    }
}