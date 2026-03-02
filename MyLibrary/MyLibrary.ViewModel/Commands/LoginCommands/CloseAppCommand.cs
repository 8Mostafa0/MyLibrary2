using System;

namespace MyLibrary.ViewModel.Commands.LoginCommands
{
    public class CloseAppCommand : CommandBase
    {
        #region Contructor
        /// <summary>
        /// </summary>
        public CloseAppCommand() { }
        #endregion



        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            //var AskMessage = MessageBox.Show("میخواهید برنامه را ببندید؟", "خروج", MessageBoxButton.YesNo);
            //if (AskMessage == MessageBoxResult.Yes)
            //{
            Environment.Exit(0);
            //}
        }
        #endregion
    }
}
