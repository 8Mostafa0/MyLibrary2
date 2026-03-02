using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public class ModalNavigationStore : IModalNavigationStore
    {
        #region Dependencies
        private IViewModelBase _currentViewModel;

        public bool IsModalOpen => CurrentViewModel != null;
        public event Action CurrentViewModelChanged;
        public IViewModelBase CurrentViewModel
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
        }

        private void OnCurrentViewModelChange()
        {
            CurrentViewModelChanged?.Invoke();

        }
        #endregion
    }
}
