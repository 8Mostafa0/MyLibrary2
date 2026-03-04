using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class SaveReservationDataCommand : CommandBase, ISaveReservationDataCommand
    {
        #region Dependencies
        private ILoanRepository _loanRepository;
        private IMessageBoxStore _messageBoxStore;
        private IReservedBooksStore _reservedBookStore;
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksRepository _reservedbooksRepository;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// validate reserve data
        /// </summary>
        /// <param name="loanRepository"></param>
        /// <param name="messageBoxStore"></param>
        /// <param name="reservedBookStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="reservedbooksRepository"></param>
        public SaveReservationDataCommand(
            ILoanRepository loanRepository,
            IMessageBoxStore messageBoxStore,
            IReservedBooksStore reservedBookStore,
            IModalNavigationStore modalNavigationStore,
            IReservedBooksRepository reservedbooksRepository
            )
        {
            _loanRepository = loanRepository;
            _messageBoxStore = messageBoxStore;
            _reservedBookStore = reservedBookStore;
            _modalNavigationStore = modalNavigationStore;
            _reservedbooksRepository = reservedbooksRepository;
        }
        #endregion

        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            #region Input Validation
            if (_reservedBookStore.SelectedClient is null)
            {
                _messageBoxStore.Show("لطفا کاربری را انتخاب کنید", "رزرو کتاب");
                return;
            }
            if (_reservedBookStore.SelectedBook is null)
            {
                _messageBoxStore.Show("لطفا کتابی را انتخاب کنید", "رزرو کتاب");
                return;
            }
            #endregion

            #region Client Validation

            if (_reservedBookStore.SelectedClient.ID != _reservedBookStore.SelectedReserv.ClientId)
            {

                if (_reservedBookStore.SelectedClient.Tier < _reservedBookStore.SelectedBook.Tier)
                {
                    _messageBoxStore.Show("فقط کاربران ویژه میتوانند کتاب رزور کنند", "رزرو کتاب");
                    return;
                }
                List<Loan> UserDilayedLoans = await _loanRepository.UserHaveDilayedLoan(_reservedBookStore.SelectedClient.ID);
                if (!(UserDilayedLoans is null) && UserDilayedLoans.Count() > 0)
                {
                    _messageBoxStore.Show("کاربر امانتی تحویل نداده و با تاخیر دارد", "رزرو کتاب");
                    return;
                }
                ReservedBook UserReservs = await _reservedbooksRepository.UserHaveReservedBook(_reservedBookStore.SelectedClient.ID);
                if (!(UserReservs is null))
                {
                    _messageBoxStore.Show("این کاربر کتابی را از قبل رزرو کرده است", "رزرو کتاب");
                    return;
                }
            }
            #endregion

            #region Book Validation
            if (_reservedBookStore.SelectedBook.ID != _reservedBookStore.SelectedReserv.BookId)
            {

                ReservedBook BookReservs = await _reservedbooksRepository.BookAlreadyRegistred(_reservedBookStore.SelectedBook.ID);
                if (!(BookReservs is null))
                {

                    _messageBoxStore.Show("کاربری این کتاب را از قبل رزور کرده است", "رزرو کتاب");
                    return;
                }

            }
            #endregion

            #region Edit ReservedBook
            if (_reservedBookStore.SelectedReserv.ID == 0)
            {

                ReservedBook reservedBook = new ReservedBook()
                {
                    BookId = _reservedBookStore.SelectedBook.ID,
                    ClientId = _reservedBookStore.SelectedClient.ID
                };

                await _reservedBookStore.AddReservBook(reservedBook);
            }
            else
            {
                _reservedBookStore.SelectedReserv.BookId = _reservedBookStore.SelectedBook.ID;
                _reservedBookStore.SelectedReserv.ClientId = _reservedBookStore.SelectedClient.ID;
                await _reservedBookStore.UpdateReservedBook(_reservedBookStore.SelectedReserv);
            }
            #endregion

            _modalNavigationStore.Close();
        }
        #endregion
    }
}
