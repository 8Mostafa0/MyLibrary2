using MyLibrary.ViewModel.Stores;
using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands.MessageBoxCommands
{
    public class MessageBoxConfrimCommand : CommandBase, IMessageBoxConfrimCommand
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private ICommand _customCommand;
        #endregion
        #region Constructor
        public MessageBoxConfrimCommand(IMessageBoxStore messageBoxStore, ICommand customCommand = null)
        {
            _messageBoxStore = messageBoxStore;
            _customCommand = customCommand;
        }
        #endregion

        #region Methods
        public override void Execute(object parameter)
        {
            _messageBoxStore.MessageBoxResult = true;
            if (_customCommand != null)
            {
                _customCommand.Execute(null);
            }
            _messageBoxStore.CloseMessageBox();
        }
        #endregion
    }
}
