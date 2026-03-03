using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class RemoveReservBookCommand : CommandBase, IRemoveReservBookCommand
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private IReservedBooksStore _reservedBooksStore;
        private IReservedBooksViewModel _reservedBooksViewModel;
        private IRemoveReservBookCommand _removeReservBookCommand;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// validate selected reserv then ask for delete
        /// </summary>
        /// <param name="messageBoxStore"></param>
        /// <param name="reservedBooksStore"></param>
        /// <param name="reservedBooksViewModel"></param>
        public RemoveReservBookCommand(
            IMessageBoxStore messageBoxStore,
            IReservedBooksStore reservedBooksStore,
            IReservedBooksViewModel reservedBooksViewModel,
            IRemoveReservBookCommand removeReservBookCommand
            )
        {
            _messageBoxStore = messageBoxStore;
            _reservedBooksStore = reservedBooksStore;
            _reservedBooksViewModel = reservedBooksViewModel;
            _removeReservBookCommand = removeReservBookCommand;
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

                _messageBoxStore.Show("آیا از حذف این نوبت مطمن هستید؟", "حذف نوبت", "بله", "خیر", _removeReservBookCommand);
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
