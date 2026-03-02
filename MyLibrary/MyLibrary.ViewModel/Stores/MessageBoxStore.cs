using MyLibrary.ViewModel.ViewModels;
using MyLibrary.ViewModel.ViewModels.MessageBoxViewModel;
using System;
using System.Windows.Input;

namespace MyLibrary.ViewModel.Stores
{
    public class MessageBoxStore : IMessageBoxStore
    {
        private IMessageBoxViewModel _messageBoxViewModel;
        public bool IsMessageOpen => _messageBoxViewModel != null;
        public bool MessageBoxResult = false;
        public event Action MessageViewModelChanged;
        public IMessageBoxViewModel MessageBoxViewModel
        {
            get => _messageBoxViewModel;
            set
            {
                _messageBoxViewModel?.Dispose();
                _messageBoxViewModel = value;
                OnMessageBoxViewChanged();
            }
        }
        public IViewModelBase Show(string title, string caption, string firstBtText = null, string secondBtText = null, ICommand command = null)
        {
            MessageBoxViewModel = new MessageBoxViewModel(this, title, caption, firstBtText, secondBtText, command);
            return MessageBoxViewModel;
        }
        public void CloseMessageBox()
        {
            MessageBoxViewModel = null;
            MessageBoxResult = false;
        }
        public void OnMessageBoxViewChanged()
        {
            MessageViewModelChanged?.Invoke();
        }

    }
}
