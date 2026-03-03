using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BooksCommands;
using System.Collections.Generic;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IBooksViewModel : IViewModelBase
    {
        IAddNewBookCommand AddNewBookCommand { get; }
        IEnumerable<Book> Books { get; }
        IDeleteBookCommand DeleteBookCommand { get; }
        IEditBookCommand EditBookCommand { get; }
        ILoadBooksCommand LoadBooksCommand { get; }
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