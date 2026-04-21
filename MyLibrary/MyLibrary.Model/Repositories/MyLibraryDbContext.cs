namespace MyLibrary.Model.Repositories
{
    public class MyLibraryDbContext : IMyLibraryDbContext
    {
        public IClientsRepository ClientsRepository { get; set; }
        public IBooksRepository BooksRepository { get; set; }
        public IReservedBooksRepository ReservedBooksRepository { get; set; }
        public ILoanRepository LoanRepository { get; set; }
        public MyLibraryDbContext(IClientsRepository clientsRepository, IBooksRepository booksRepository, IReservedBooksRepository reservedBooksRepository, ILoanRepository loanRepository)
        {
            ClientsRepository = clientsRepository;
            BooksRepository = booksRepository;
            ReservedBooksRepository = reservedBooksRepository;
            LoanRepository = loanRepository;
        }


        public void Dispose()
        {
            BooksRepository?.Dispose();

        }
    }
}
