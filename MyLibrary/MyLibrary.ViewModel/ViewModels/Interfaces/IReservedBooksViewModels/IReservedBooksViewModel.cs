using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BaseCommands;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public interface IReservedBooksViewModel : IViewModelBase
    {
        AsyncRelayCommand AddNewReservBookCommand { get; }
        string BookName { get; set; }
        IViewModelBase CurrentModalViewModel { get; }
        AsyncRelayCommand EditeReservBookCommand { get; }
        bool IsModalOpen { get; }
        AsyncRelayCommand RemoveReservBookCommand { get; }
        IEnumerable<ReservedBook> ReservedBooks { get; }
        AsyncRelayCommand ResetReservBookCommand { get; }
        AsyncRelayCommand SearchBookNameInReservedBookCommand { get; }
        ReservedBook SelectedReservedBook { get; set; }

    }
}