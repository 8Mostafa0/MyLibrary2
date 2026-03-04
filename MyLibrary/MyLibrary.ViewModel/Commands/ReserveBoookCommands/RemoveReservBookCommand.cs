using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class RemoveReservBookCommand : CommandBase, IRemoveReservBookCommand
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private IReservedBooksStore _reservedBooksStore;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// validate selected reserv then ask for delete
        /// </summary>
        /// <param name="messageBoxStore"></param>
        /// <param name="reservedBooksStore"></param>
        public RemoveReservBookCommand(
            IMessageBoxStore messageBoxStore,
            IReservedBooksStore reservedBooksStore
            )
        {
            _messageBoxStore = messageBoxStore;
            _reservedBooksStore = reservedBooksStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            if (_reservedBooksStore.SelectedReserv is null)
            {
                _messageBoxStore.Show("لطفا نوبتی را برای حذف انتخاب کنید", "حذف نوبت");
            }
            else
            {

                _messageBoxStore.Show("آیا از حذف این نوبت مطمن هستید؟", "حذف نوبت", "بله", "خیر", this);
                if (_messageBoxStore.MessageBoxResult)
                {
                    _messageBoxStore.CloseMessageBox();
                    await _reservedBooksStore.DeleteReservBook(_reservedBooksStore.SelectedReserv);
                }
            }
        }
        #endregion
    }
}
