using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class AddNewReservBookCommand : CommandBase, IAddNewReservBookCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IClientsStore _clientsStore;
        private IReservedBooksStore _reservedBooksStore;
        private IModalNavigationStore _modalNavigationStore;
        private IReservedBooksRepository _reservedBooksRepository;
        private ILoanRepository _loanRepository;
        private IClientsRepository _clientsRepository;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load addedite reserved book view model to modal navigation view
        /// </summary>
        public AddNewReservBookCommand()
        {
            _booksStore = ClassFactory.CreateBooksStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            _reservedBooksRepository = ClassFactory.CreateReservedBooksRepository();
            _loanRepository = ClassFactory.CreateLoanRepository();
            _clientsRepository = ClassFactory.CreateClientsRepository();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>

        public override void Execute(object parameter)
        {
            //_modalNavigationStore.CurrentViewModel = AddEditeReserveBookViewModel.LoadViewModel(_modalNavigationStore, _reservedBooksStore, _clientsStore, _booksStore, _loanRepository, _reservedBooksRepository, _clientsRepository, _messageBoxStore, null);
        }
        #endregion
    }
}
