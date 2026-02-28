using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public class ModalNavigationStore
    {
        #region Dependencies
        private ViewModelBase _currentViewModel;
        public bool IsModalOpen => CurrentViewModel != null;
        public event Action CurrentViewModelChanged;
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


        #endregion




        #region Methods 

        public void Close()
        {
            CurrentViewModel = null;
            OnCurrentViewModelChange();
        }

        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();

        }
        #endregion
    }
}
