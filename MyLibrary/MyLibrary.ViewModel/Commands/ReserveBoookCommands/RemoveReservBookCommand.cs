using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.ReservedBooksViewModels;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class RemoveReservBookCommand : CommandBase, IRemoveReservBookCommand
    {
        #region Dependencies
        private IReservedBooksStore _reservedBooksStore;
        private IReservedBooksViewModel _reservedBooksViewModel;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// validate selected reserv then ask for delete
        /// </summary>
        public RemoveReservBookCommand()
        {
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
            _reservedBooksViewModel = ClassFactory.CreateReservedBooksViewModel();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
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

                _messageBoxStore.Show("آیا از حذف این نوبت مطمن هستید؟", "حذف نوبت", "", "", ClassFactory.CreateRemoveReservBookCommand());
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
