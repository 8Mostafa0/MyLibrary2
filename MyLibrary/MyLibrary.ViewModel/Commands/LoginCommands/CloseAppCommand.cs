using MyLibrary.ViewModel.Stores;
using System;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public class CloseAppCommand : CommandBase, ICloseAppCommand
    {
        #region Dependencies
        private IMessageBoxStore _messageBoxStore;
        private ICloseAppCommand _closeAppCommand;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageBoxStore"></param>
        public CloseAppCommand(IMessageBoxStore messageBoxStore, ICloseAppCommand closeAppCommand)
        {
            _messageBoxStore = messageBoxStore;
            _closeAppCommand = closeAppCommand;
        }
        #endregion



        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _messageBoxStore.Show("میخواهید برنامه را ببندید؟", "خروج", "بله", "خیر", _closeAppCommand);
            if (_messageBoxStore.MessageBoxResult)
            {
                _messageBoxStore.CloseMessageBox();
                Environment.Exit(0);
            }
        }
        #endregion
    }
}
