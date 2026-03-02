using MyLibrary.Model.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IBooksViewModel : IViewModelBase
    {
        ICommand AddNewBookCommand { get; }
        IEnumerable<Book> Books { get; }
        ICommand DeleteBookCommand { get; }
        ICommand EditBookCommand { get; }
        ICommand LoadBooksCommand { get; }
        string Name { get; set; }
        ICommand OrderBooksCommand { get; }
        string PublicationDate { get; set; }
        string Publisher { get; set; }
        ICommand ReloadClientsCommand { get; }
        Book SelectedBook { get; set; }
        int SortIndex { get; set; }
        string Subject { get; set; }
        int Tier { get; set; }
        void AddNewBook(Book book);
        void UpdateBooks();
    }
}