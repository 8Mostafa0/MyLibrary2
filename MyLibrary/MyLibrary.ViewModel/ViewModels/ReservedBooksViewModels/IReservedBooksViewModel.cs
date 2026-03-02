using MyLibrary.ViewModel.ViewModels.ModelsViewModels;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels
{
    public interface IReservedBooksViewModel : IViewModelBase
    {
        ICommand AddNewReservBookCommand { get; }
        string BookName { get; set; }
        IViewModelBase CurrentModalViewModel { get; }
        ICommand EditeReservBookCommand { get; }
        bool IsModalOpen { get; }
        ICommand LoadReservedBooksCommand { get; }
        ICommand RemoveReservBookCommand { get; }
        IEnumerable<ReservedBookViewModel> ReservedBooks { get; }
        ICommand ResetReservBookCommand { get; }
        ICommand SearchBookNameInReservedBookCommand { get; }
        ReservedBookViewModel SelectedReservedBook { get; set; }

    }
}