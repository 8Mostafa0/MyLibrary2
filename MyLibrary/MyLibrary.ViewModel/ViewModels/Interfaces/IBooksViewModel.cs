using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BaseCommands;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IBooksViewModel : IViewModelBase
    {
        AsyncRelayCommand AddNewBookCommand { get; }
        IEnumerable<Book> Books { get; }
        AsyncRelayCommand DeleteBookCommand { get; }
        AsyncRelayCommand EditBookCommand { get; }
        string Name { get; set; }
        AsyncRelayCommand OrderBooksCommand { get; }
        string PublicationDate { get; set; }
        string Publisher { get; set; }
        AsyncRelayCommand ReloadBooksCommand { get; }
        Book SelectedBook { get; set; }
        int SortIndex { get; set; }
        string Subject { get; set; }
        int Tier { get; set; }
    }
}