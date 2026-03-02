using MyLibrary.ViewModel.ViewModels;
using MyLibrary.ViewModel.ViewModels.MessageBoxViewModel;
using System;
using System.Windows.Input;

namespace MyLibrary.ViewModel.Stores
{
    public interface IMessageBoxStore
    {
        bool MessageBoxResult { get; set; }
        bool IsMessageOpen { get; }
        IMessageBoxViewModel MessageBoxViewModel { get; set; }

        event Action MessageViewModelChanged;
        void CloseMessageBox();
        void OnMessageBoxViewChanged();
        IViewModelBase Show(string title, string caption, string firstBtText = null, string secondBtText = null, ICommand command = null);
    }
}