using MyLibrary.ViewModel.Stores;
using System;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public class CloseAppCommand : CommandBase
    {
        #region Dependencies
        private MessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// </summary>
        public CloseAppCommand(MessageBoxStore messageBoxStore)
        {
            _messageBoxStore = messageBoxStore;
        }
        #endregion



        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _messageBoxStore.Show("میخواهید برنامه را ببندید؟", "خروج", "بله", "خیر", new CloseAppCommand(_messageBoxStore));
            if (_messageBoxStore.MessageBoxResult)
            {
                _messageBoxStore.CloseMessageBox();
                Environment.Exit(0);
            }
        }
        #endregion
    }
}
