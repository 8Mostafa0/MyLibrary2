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

        private ViewModelBase _mainContentViewModel;
        private ViewModelBase _contentScreen;
        private ViewModelBase _statusBarViewModel;

        public ViewModelBase StatusBarViewModel
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

        public ViewModelBase MainContentViewModel
        {
            get => _mainContentViewModel;
            set
            {
                _mainContentViewModel?.Dispose();
                _mainContentViewModel = value;
                OnMainContentViewModelChanged();
            }
        }
        public ViewModelBase ContentScreen
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
