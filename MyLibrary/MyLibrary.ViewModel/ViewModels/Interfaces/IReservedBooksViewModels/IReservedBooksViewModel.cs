using MyLibrary.ViewModel.Commands.ReserveBoookCommands;
using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public interface IReservedBooksViewModel : IViewModelBase
    {
        IAddNewReservBookCommand AddNewReservBookCommand { get; }
        string BookName { get; set; }
        IViewModelBase CurrentModalViewModel { get; }
        IEditeReservBookCommand EditeReservBookCommand { get; }
        bool IsModalOpen { get; }
        ILoadReservedBooksCommand LoadReservedBooksCommand { get; }
        IRemoveReservBookCommand RemoveReservBookCommand { get; }
        IEnumerable<ReservedBookViewModel> ReservedBooks { get; }
        ICommand ResetReservBookCommand { get; }
        ICommand SearchBookNameInReservedBookCommand { get; }
        ReservedBookViewModel SelectedReservedBook { get; set; }

    }
}