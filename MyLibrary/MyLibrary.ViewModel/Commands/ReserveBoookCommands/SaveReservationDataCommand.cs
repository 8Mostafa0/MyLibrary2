using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class SaveReservationDataCommand : CommandBase, ISaveReservationDataCommand
    {
        #region Dependencies
        private ILoanRepository _loanRepository;
        private IMessageBoxStore _messageBoxStore;
        private IClientsRepository _clientsRepository;
        private IReservedBooksStore _reservedBookStore;
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksRepository _reservedbooksRepository;
        private IAddEditeReserveBookViewModel _addediteReserveBookViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// validate reserve data
        /// </summary>
        public SaveReservationDataCommand()
        {
            _loanRepository = ClassFactory.CreateLoanRepository();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _clientsRepository = ClassFactory.CreateClientsRepository();
            _reservedBookStore = ClassFactory.CreateReservedBooksStore();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            _reservedbooksRepository = ClassFactory.CreateReservedBooksRepository();
            _addediteReserveBookViewModel = ClassFactory.CreateAddEditeReserveBookViewModel();
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
            if (_addediteReserveBookViewModel.SelectedClient == null)
            {
                _messageBoxStore.Show("لطفا کاربری را انتخاب کنید", "رزرو کتاب");
                return;
            }
            if (_addediteReserveBookViewModel.SelectedBook == null)
            {
                _messageBoxStore.Show("لطفا کتابی را انتخاب کنید", "رزرو کتاب");
                return;
            }
            #endregion

            #region Client Validation

            if (_addediteReserveBookViewModel.SelectedClient.ID != _addediteReserveBookViewModel.SelectedReservedBook.ClientId)
            {

                if (_addediteReserveBookViewModel.SelectedClient.Tier < _addediteReserveBookViewModel.SelectedBook.Tier)
                {
                    _messageBoxStore.Show("فقط کاربران ویژه میتوانند کتاب رزور کنند", "رزرو کتاب");
                    return;
                }
                List<Loan> UserDilayedLoans = await _loanRepository.UserHaveDilayedLoan(_addediteReserveBookViewModel.SelectedClient.ID);
                if (!(UserDilayedLoans is null) && UserDilayedLoans.Count() > 0)
                {
                    _messageBoxStore.Show("کاربر امانتی تحویل نداده و با تاخیر دارد", "رزرو کتاب");
                    return;
                }
                ReservedBook UserReservs = await _reservedbooksRepository.UserHaveReservedBook(_addediteReserveBookViewModel.SelectedClient.ID);
                if (!(UserReservs is null))
                {
                    _messageBoxStore.Show("این کاربر کتابی را از قبل رزرو کرده است", "رزرو کتاب");
                    return;
                }
            }
            #endregion

            #region Book Validation
            if (_addediteReserveBookViewModel.SelectedBook.ID != _addediteReserveBookViewModel.SelectedReservedBook.BookId)
            {

                ReservedBook BookReservs = await _reservedbooksRepository.BookAlreadyRegistred(_addediteReserveBookViewModel.SelectedBook.ID);
                if (!(BookReservs is null))
                {

                    _messageBoxStore.Show("کاربری این کتاب را از قبل رزور کرده است", "رزرو کتاب");
                    return;
                }

            }
            #endregion

            #region Edit ReservedBook
            if (_addediteReserveBookViewModel.SelectedReservedBook.ID == 0)
            {

                ReservedBook reservedBook = new ReservedBook()
                {
                    BookId = _addediteReserveBookViewModel.SelectedBook.ID,
                    ClientId = _addediteReserveBookViewModel.SelectedClient.ID
                };

                await _reservedBookStore.AddReservBook(reservedBook);
            }
            else
            {
                _addediteReserveBookViewModel.SelectedReservedBook.BookId = _addediteReserveBookViewModel.SelectedBook.ID;
                _addediteReserveBookViewModel.SelectedReservedBook.ClientId = _addediteReserveBookViewModel.SelectedClient.ID;
                await _reservedBookStore.UpdateReservedBook(_addediteReserveBookViewModel.SelectedReservedBook);
            }
            #endregion

            _modalNavigationStore.Close();
        }
        #endregion
    }
}
