namespace MyLibrary.Model.Repositories
{
    public interface IMyLibraryDbContext
    {
        IBooksRepository BooksRepository { get; set; }
        IClientsRepository ClientsRepository { get; set; }
        ILoanRepository LoanRepository { get; set; }
        IReservedBooksRepository ReservedBooksRepository { get; set; }

        void Dispose();
    }
}