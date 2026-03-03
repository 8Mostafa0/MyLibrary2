using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class SaveLoanDataCommand : CommandBase, ISaveLoanDataCommand
    {
        #region Dependencies
        private readonly ILoansStore _loansStore;
        private readonly ISettingsStore _settingsStore;
        private readonly ILoanRepository _loanRepository;
        private readonly IBooksRepository _bookRepository;
        private readonly IModalNavigationStore _modalNavigationStore;
        private readonly IAddEditeLoanViewModel _addEditeLoanViewModel;
        private readonly IReservedBooksRepository _reservedBooksRepository;
        private readonly IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// validate loan data from book and client then remove reserved data in ReservedBook table and then add new loan to Loans table
        /// </summary>
        /// <param name="addEditeLoanViewModel"></param>
        /// <param name="loansStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="settingsStore"></param>
        /// <param name="booksRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public SaveLoanDataCommand(
            ILoansStore loansStore,
            ISettingsStore settingsStore,
            ILoanRepository loanRepository,
            IMessageBoxStore messageBoxStore,
            IBooksRepository booksRepository,
            IModalNavigationStore modalNavigationStore,
            IAddEditeLoanViewModel addEditeLoanViewModel,
            IReservedBooksRepository reservedBooksRepository)
        {
            _loansStore = loansStore;
            _settingsStore = settingsStore;
            _loanRepository = loanRepository;
            _bookRepository = booksRepository;
            _messageBoxStore = messageBoxStore;
            _modalNavigationStore = modalNavigationStore;
            _addEditeLoanViewModel = addEditeLoanViewModel;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            try
            {
                #region Inputs Validation
                if (_addEditeLoanViewModel.SelectedBook is null)
                {
                    _messageBoxStore.Show("لطفا کتابی را برای امانت انتخاب کنید", "ثبت امانت");
                    return;
                }
                if (_addEditeLoanViewModel.SelectedClient is null)
                {
                    _messageBoxStore.Show("لطفا کاربری را برای امانت انتخاب کنید", "ثبت امانت");
                    return;
                }
                #endregion


                Dictionary<string, int> LoanSettings = _settingsStore.GetLoansSetting();


                #region Date Validation

                DateTime ReturnDate = _addEditeLoanViewModel.ReturnDate;
                if ((ReturnDate - DateTime.Now).Days > LoanSettings["MaxLoanDays"])
                {
                    _messageBoxStore.Show($"لطفا تاریخ برگشت را زودتر انتخاب کنید حداکثر{LoanSettings["MaxLoanDays"]}روز", "ثبت امانت");
                    return;
                }
                else if (ReturnDate < DateTime.Now)
                {
                    _messageBoxStore.Show("تاریخ بازگشت نمیتواند در تاریخ گذشته باشد.", "ثبت امانت");
                    return;
                }

                #endregion

                #region Client Validateion

                if (_addEditeLoanViewModel.SelectedLoan.ClientId != _addEditeLoanViewModel.SelectedClient.ID)
                {
                    if (_addEditeLoanViewModel.SelectedBook.Tier > _addEditeLoanViewModel.SelectedClient.Tier)
                    {
                        _messageBoxStore.Show("این کتاب برای کاربران ویژه است", "ثبت امانت");
                        return;
                    }

                    List<Loan> AllUserLoans = await _loanRepository.GetAllClientLoans(_addEditeLoanViewModel.SelectedClient.ID);
                    if (!(AllUserLoans is null) && AllUserLoans.Count() > LoanSettings["MaxBooksLoan"])
                    {
                        _messageBoxStore.Show("این کاربر به حداکثر تعداد امانت فعال رسیده است", "ثبت امانت");
                        return;
                    }
                    List<Loan> Dilayedloans = await _loanRepository.UserHaveDilayedLoan(_addEditeLoanViewModel.SelectedClient.ID);
                    if (!(Dilayedloans is null) && Dilayedloans.Count() > 0)
                    {

                        _messageBoxStore.Show("این کاربر امانتی تحویل نشده و با تاخیر دارد", "ثبت امانت");
                        return;
                    }
                }
                #endregion

                #region Book Validation
                if (_addEditeLoanViewModel.SelectedLoan.BookId != _addEditeLoanViewModel.SelectedBook.ID)
                {
                    List<Book> BookCopies = await _bookRepository.GetBooksByName(_addEditeLoanViewModel.SelectedBook.Name);
                    int NotReturnedLoansCount = 0;
                    foreach (Book book in BookCopies)
                    {
                        List<Loan> NotReturnedLoans = await _loanRepository.GetNotReturnedLoanOfBook(book.ID);
                        NotReturnedLoansCount += NotReturnedLoans.Count();
                    }

                    List<ReservedBook> BookReservs = await _reservedBooksRepository.GetReservationForBook(_addEditeLoanViewModel.SelectedBook.ID);

                    if (!(BookCopies is null) && BookCopies.Count() - 1 < NotReturnedLoansCount)
                    {
                        _messageBoxStore.Show("تمامی نسخه های این کتاب به امانت داده شده اند", "ثبت امانت");
                        return;
                    }
                    bool IsUserReservedBook = false;
                    if (BookReservs.Count > 0)
                    {
                        foreach (ReservedBook reservedBook in BookReservs)
                        {
                            if (reservedBook.ClientId == _addEditeLoanViewModel.SelectedClient.ID)
                            {
                                IsUserReservedBook = false;
                                break;
                            }
                            else
                            {
                                IsUserReservedBook = true;
                            }
                        }
                    }

                    if (!(BookReservs is null) && BookReservs.Count() >= (BookCopies.Count() - NotReturnedLoansCount) - 1 && IsUserReservedBook)
                    {
                        _messageBoxStore.Show("کاربری این کتاب را رزور کرده است", "ثبت امانت");
                        return;
                    }

                    if (!(BookReservs is null) && BookReservs.Any())
                    {
                        await _reservedBooksRepository.DeleteReservedBookWithClientToDb(BookReservs.SingleOrDefault(r => r.ClientId == _addEditeLoanViewModel.SelectedClient.ID));
                    }
                }

                #endregion


                #region Save Book
                Loan loan = new Loan()
                {
                    ClientId = _addEditeLoanViewModel.SelectedClient.ID,
                    BookId = _addEditeLoanViewModel.SelectedBook.ID,
                    ReturnDate = _addEditeLoanViewModel.ReturnDate,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                if (_addEditeLoanViewModel.SelectedLoan.Id == 0)
                {
                    await _loansStore.AddLoan(loan);

                }
                else
                {
                    loan.Id = _addEditeLoanViewModel.SelectedLoan.Id;
                    await _loansStore.UpdateLoan(loan);
                }
                _addEditeLoanViewModel.SelectedBook = null;
                _addEditeLoanViewModel.SelectedClient = null;
                _addEditeLoanViewModel.ReturnDate = DateTime.Now;
                #endregion

                ClassFactory.CreateCloseModalCommand().Execute(null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
