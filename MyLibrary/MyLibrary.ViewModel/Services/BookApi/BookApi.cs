using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using Newtonsoft.Json.Linq;
using System.Net.Http;
namespace MyLibrary.ViewModel.Services.BookApi
{
    public class BookApi : IBookApi
    {
        private IBookApiStore _bookApiStore;
        public BookApi(IBookApiStore bookApiStore)
        {
            _bookApiStore = bookApiStore;
        }

        public async void GetBookInfo(int bookNumber)
        {
            const string apiUrl = "https://gutendex.com/books/";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}{bookNumber}");
                    response.EnsureSuccessStatusCode();
                    await response.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        JObject data = JObject.Parse(t.Result);
                        NewBook book = new NewBook
                        {
                            ID = (int)data["id"],
                            Name = (string)data["title"],
                            Publisher = string.Join(", ", data["authors"][0]),
                            Desciption = string.Join(", ", data["summaries"])
                        };
                        _bookApiStore.BookData = book;
                    });
                }
                catch (HttpRequestException e)
                {
                    _bookApiStore.BookData = new NewBook()
                    { ID = 0, Name = "Error", Desciption = e.ToString() };
                }
            }
        }
    }
}
