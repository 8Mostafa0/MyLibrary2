using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Services.BookApi
{
    public interface IBookApi
    {
        Task GetBookInfo(int bookNumber);
    }
}