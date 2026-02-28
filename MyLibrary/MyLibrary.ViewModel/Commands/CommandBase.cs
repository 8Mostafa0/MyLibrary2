using System;
using System.Windows.Input;

namespace MyLibrary.ViewModel.Commands
{
    public abstract class CommandBase : ICommand
    {
        #region Properties
        public event EventHandler? CanExecuteChanged;
        #endregion

        #region Methods
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);


        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
        #endregion
    }
}
