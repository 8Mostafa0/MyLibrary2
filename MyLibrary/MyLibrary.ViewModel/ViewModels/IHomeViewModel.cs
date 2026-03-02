namespace MyLibrary.ViewModel.ViewModels
{
    public interface IHomeViewModel : IViewModelBase
    {
        string BooksCount { get; set; }
        string ClientsCount { get; set; }
        string DilayedLoanCount { get; set; }
        string LoansCount { get; set; }
        string ShowBooksCount { get; }
        string ShowClientsCount { get; set; }
        string ShowDilayedLoanCount { get; set; }
        string ShowLoansCount { get; set; }
    }
}