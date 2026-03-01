using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public class ModalNavigationStore
    {
        #region Dependencies
        private ViewModelBase _currentViewModel;
        private ViewModelBase _messageBoxViewModel;

        public bool IsMessageOpen = > false;
        public bool IsModalOpen => CurrentViewModel != null;
        public event Action CurrentViewModelChanged;
        public event Action MessageViewModelChanged;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChange();
            }
        }
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

        public void CloseMessageBox()
        {
            MessageBoxViewModel = null;
        }
        private void OnMessageBoxViewChanged()
        {
            MessageViewModelChanged?.Invoke();
        }

        #endregion




        #region Methods 

        public void Close()
        {
            CurrentViewModel = null;
        }

        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();

        }
        #endregion
    }
}
