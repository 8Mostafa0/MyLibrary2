using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.MessageBoxCommands
{
    internal class MessageBoxConfrimCommand : CommandBase
    {
        #region Dependencies
        private MessageBoxStore _messageBoxStore;
        #endregion
        #region Constructor
        public MessageBoxConfrimCommand(MessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
        }
        #endregion

        #region Methods
        public override void Execute(object parameter)
        {
            _messageBoxStore.MessageBoxResult = true;
            _messageBoxStore.CloseMessageBox();
        }
        #endregion
    }
}
