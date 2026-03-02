using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public class NavigationStore
    {
        #region Dependencies
        public event Action MainContentViewModelChanged;
        public event Action ContentViewModelChanged;
        public event Action StatusBarViewModelChanged;

        private IViewModelBase _mainContentViewModel;
        private IViewModelBase _contentScreen;
        private IViewModelBase _statusBarViewModel;

        public IViewModelBase StatusBarViewModel
        {
            get => _statusBarViewModel;
            set
            {
                _statusBarViewModel = value;
                OnStatusBarViewModelChanged();
            }
        }

        private void OnStatusBarViewModelChanged()
        {
            StatusBarViewModelChanged?.Invoke();
        }

        public IViewModelBase MainContentViewModel
        {
            get => _mainContentViewModel;
            set
            {
                _mainContentViewModel?.Dispose();
                _mainContentViewModel = value;
                OnMainContentViewModelChanged();
            }
        }
        public IViewModelBase ContentScreen
        {
            get => _contentScreen;
            set
            {
                if (_contentScreen != value)
                {
                    _contentScreen?.Dispose();
                    _contentScreen = value;
                    OnCurrentViewModelChanged();
                }
            }
        }

        #endregion


        #region Methods
        private void OnMainContentViewModelChanged()
        {
            MainContentViewModelChanged?.Invoke();
        }


        private void OnCurrentViewModelChanged()
        {
            ContentViewModelChanged?.Invoke();
        }

        #endregion

    }
}
