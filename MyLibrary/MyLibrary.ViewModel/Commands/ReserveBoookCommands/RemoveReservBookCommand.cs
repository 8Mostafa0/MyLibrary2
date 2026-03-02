using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class RemoveReservBookCommand : CommandBase
    {
        #region Dependencies
        private ReservedBooksStore _reservedBooksStore;
        private ReservedBooksViewModel _reservedBooksViewModel;
        private MessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// validate selected reserv then ask for delete
        /// </summary>
        /// <param name="reservedBooksViewModel"></param>
        /// <param name="reservedBooksStore"></param>
        public RemoveReservBookCommand(ReservedBooksViewModel reservedBooksViewModel, ReservedBooksStore reservedBooksStore, MessageBoxStore messageBoxStore)
        {
            _reservedBooksStore = reservedBooksStore;
            _reservedBooksViewModel = reservedBooksViewModel;
            _messageBoxStore = messageBoxStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            if (_reservedBooksViewModel.SelectedReservedBook is null)
            {
                _messageBoxStore.Show("لطفا نوبتی را برای حذف انتخاب کنید", "حذف نوبت");
            }
            else
            {

                _messageBoxStore.Show("آیا از حذف این نوبت مطمن هستید؟", "حذف نوبت", "", "", new RemoveReservBookCommand(_reservedBooksViewModel, _reservedBooksStore, _messageBoxStore));
                if (_messageBoxStore.MessageBoxResult)
                {
                    _messageBoxStore.CloseMessageBox();
                    await _reservedBooksStore.DeleteReservBook(_reservedBooksViewModel.SelectedReservedBook.ToReservedBook());
                }
            }
        }
        #endregion
    }
}
