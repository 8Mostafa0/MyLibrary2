using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.MessageBoxCommands
{
    public class CloseMessageBox : CommandBase
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Constructor
        public CloseMessageBox(IMessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
        }
        #endregion

        #region Methods
        public override void Execute(object parameter)
        {
            if (_messageBoxStore.MessageBoxViewModel != null)
            {
                _messageBoxStore.MessageBoxResult = false;
            }
            _messageBoxStore.CloseMessageBox();
        }
        #endregion
    }
}
