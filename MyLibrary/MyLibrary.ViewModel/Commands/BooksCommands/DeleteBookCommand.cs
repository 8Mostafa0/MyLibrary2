using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class DeleteBookCommand : CommandBase
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private LoanRepository _loanRepository;
        private IBooksViewModel _booksViewModel;
        private ReservedBooksRepository _reservedBooksRepository;
        private MessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksViewModel"></param>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public DeleteBookCommand(IBooksViewModel booksViewModel, IBooksStore booksStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository, MessageBoxStore messageBoxStore)
        {
            _booksStore = booksStore;
            _loanRepository = loanRepository;
            _booksViewModel = booksViewModel;
            _reservedBooksRepository = reservedBooksRepository;
            _messageBoxStore = messageBoxStore;
        }

        #endregion


        #region Execution
        /// <summary>
        /// Checks To Validate Book Data First .
        /// 1_ Delete All Reservs
        /// 2_ Delete All Loans With This Book
        /// 3_ Delete This Copie Of Book Using Book Id
        /// </summary>
        /// <param name="parameter">No Perameer Needed This Method Gets Its Data From BooksViewModel 
        /// Data : Name,Publisher,Subject,PublicationDate</param>
        public override async void Execute(object parameter)
        {
            if (_booksViewModel.SelectedBook is null)
            {
                _messageBoxStore.Show("لطفا کتابی را برای حذف انتخاب کنید", "حذف کتاب");
                return;
            }

            List<Loan> BookLoans = await _loanRepository.GetNotReturnedLoanOfBook(_booksViewModel.SelectedBook.ID);
            if (BookLoans.Count > 0)
            {
                _messageBoxStore.Show("این کتاب امانتی تحویل نشده فعال دارد", "حذف کتاب");
                return;
            }
            _messageBoxStore.Show("آیا از حذف این کتاب مطمن هستید؟", "حذف کتاب", "بله", "خیر", new DeleteBookCommand(_booksViewModel, _booksStore, _loanRepository, _reservedBooksRepository, _messageBoxStore));
            if (_messageBoxStore.MessageBoxResult)
            {
                _messageBoxStore.CloseMessageBox();
                await _reservedBooksRepository.DeleteReservedBookToDb(_booksViewModel.SelectedBook.ID);
                await _loanRepository.RemoveBookLoans(_booksViewModel.SelectedBook.ID);
                await _booksStore.DeleteBook(_booksViewModel.SelectedBook);

            }
        }
        #endregion
    }
}
