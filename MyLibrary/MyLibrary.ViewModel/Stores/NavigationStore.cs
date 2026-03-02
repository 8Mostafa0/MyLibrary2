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
        private IStatusBarViewModel _StatusBarViewModel;

        public IStatusBarViewModel StatusBarViewModel
        {
            get => _StatusBarViewModel;
            set
            {
                _StatusBarViewModel = value;
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
