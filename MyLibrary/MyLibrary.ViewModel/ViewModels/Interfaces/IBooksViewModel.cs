using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.ViewModels.Interfaces
{
    public interface IBooksViewModel : IViewModelBase
    {
        IAddNewBookCommand AddNewBookCommand { get; }
        IEnumerable<IBook> Books { get; }
        IDeleteBookCommand DeleteBookCommand { get; }
        IEditBookCommand EditBookCommand { get; }
        ILoadBooksCommand LoadBooksCommand { get; }
        string Name { get; set; }
        IOrderBooksByStateCommand OrderBooksCommand { get; }
        string PublicationDate { get; set; }
        string Publisher { get; set; }
        IReloadBooksCommand ReloadBooksCommand { get; }
        IBook SelectedBook { get; set; }
        int SortIndex { get; set; }
        string Subject { get; set; }
        int Tier { get; set; }

        static abstract IBooksViewModel LoadViewModel(IBooksStore booksStore, ILoanRepository loanRepository, IReservedBooksRepository reservedBooksRepository, IBooksRepository booksRepository);
        void AddNewBook(IBook book);
        void UpdateBooks();
    }
}
}
