using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class EditeReservBookCommand : CommandBase, IEditeReservBookCommand
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private IReservedBooksStore _reservedBooksStore;
        private IModalNavigationStore _modalNavigationStore;
        private IAddEditeReserveBookViewModel _addEditeReserveBookViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// validate selected reserv and fill to the add edite view modal
        /// </summary>
        public EditeReservBookCommand(
            IMessageBoxStore messageBoxStore,
            IReservedBooksStore reservedBooksStore,
            IModalNavigationStore modalNavigationStore,
            IAddEditeReserveBookViewModel addEditeReserveBookViewModel
            )
        {
            _messageBoxStore = messageBoxStore;
            _reservedBooksStore = reservedBooksStore;
            _modalNavigationStore = modalNavigationStore;
            _addEditeReserveBookViewModel = addEditeReserveBookViewModel;
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            if (_reservedBooksStore.SelectedReserv is null)
            {
                _messageBoxStore.Show("لطفا ابتدا نوبتی را برای ویراش انتخاب کنید", "ویرایش رزرو");
            }
            else
            {
                _addEditeReserveBookViewModel.LoadBooksCommand.Execute(null);
                _addEditeReserveBookViewModel.LoadClientsCommand.Execute(null);
                _modalNavigationStore.CurrentViewModel = _addEditeReserveBookViewModel;
            }
        }
        #endregion
    }
}
