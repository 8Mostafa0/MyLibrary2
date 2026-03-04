using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Commands.BooksCommands;
using System.Collections.Generic;

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
        IOrderBooksByStateCommand OrderBooksCommand { get; }
        string PublicationDate { get; set; }
        string Publisher { get; set; }
        IReloadBooksCommand ReloadBooksCommand { get; }
        Book SelectedBook { get; set; }
        int SortIndex { get; set; }
        string Subject { get; set; }
        int Tier { get; set; }
        void AddNewBook(Book book);
        void UpdateBooks();
    }
}