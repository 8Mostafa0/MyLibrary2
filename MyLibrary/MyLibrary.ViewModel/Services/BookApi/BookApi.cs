using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace MyLibrary.ViewModel.Services.BookApi
{
    public class BookApi : IBookApi
    {
        private IBookApiStore _bookApiStore;
        public BookApi(IBookApiStore bookApiStore)
        {
            _bookApiStore = bookApiStore;
        }

        public async Task GetBookInfo(int bookNumber)
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
                        NewBook book = new NewBook();
                        if (data.ContainsKey("ID"))
                        {
                            book.ID = (int)data["id"];
                        }
                        if (data.ContainsKey("title"))
                        {
                            book.Name = (string)data["title"];
                        }
                        if (data.ContainsKey("authors") && data["authors"].Count() > 0)
                        {

                            book.Publisher = JsonConvert.SerializeObject(data["authors"][0].Value<string>("name"));
                        }
                        if (data.ContainsKey("summaries") && data["summaries"].Count() > 0)
                        {
                            book.Desciption = string.Join(", ", data["summaries"]);
                        }
                        _bookApiStore.BookData = book;
                    });
                }
                catch (HttpRequestException e)
                {
                    _bookApiStore.BookData = new NewBook()
                    { ID = 0, Name = "کتاب موجود نیست", Desciption = "خطا دریافت کتاب" + bookNumber };
                }
            }
        }
    }
}
