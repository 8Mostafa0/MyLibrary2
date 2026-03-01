using MyLibrary.ViewModel.ViewModels;
using MyLibrary.ViewModel.ViewModels.MessageBox;
using System;
using System.Windows.Input;

namespace MyLibrary.ViewModel.Stores
{
    public class MessageBoxStore
    {
        private ViewModelBase _messageBoxViewModel;
        public bool IsMessageOpen => _messageBoxViewModel != null;
        public event Action MessageViewModelChanged;
        public ViewModelBase MessageBoxViewModel
        {
            get => _messageBoxViewModel;
            set
            {
                _messageBoxViewModel?.Dispose();
                _messageBoxViewModel = value;
                OnMessageBoxViewChanged();
            }
        }
        public void Show(string title, string caption, string firstBtText = null, ICommand firstBtCommand = null, string secondBtText = null, ICommand secondBtCommand = null)
        {
            MessageBoxViewModel = new MessageBox(title, caption, firstBtText, firstBtCommand, secondBtText, secondBtCommand);
        }
        public void CloseMessageBox()
        {
            MessageBoxViewModel = null;
        }
        private void OnMessageBoxViewChanged()
        {
            MessageViewModelChanged?.Invoke();
        }

    }
}
