using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.MessageBoxCommands
{
    public class CloseMessageBox : CommandBase
    {
        #region Dependencies
        private MessageBoxStore _messageBoxStore;
        #endregion


        #region Constructor
        public CloseMessageBox(MessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
        }
        #endregion

        #region Methods
        public override void Execute(object parameter)
        {
            _messageBoxStore.CloseMessageBox();
        }
        #endregion
    }
}
