using MyLibrary.ViewModel.Stores.Interfaces;
using MyLibrary.ViewModel.ViewModels.Interfaces;

namespace MyLibrary.ViewModel.ViewModels
{
    public class LayoutViewModel : ViewModelBase, ILayoutViewModel
    {
        #region Dependencies
        private readonly INavigationStore _navigationStore;
        public IViewModelBase contentViewModel => _navigationStore.ContentScreen;
        public INavigationBarViewModel MainContentViewModel => _navigationStore.MainContentViewModel;

        public IStatusBarViewModel StatusBarViewModel => _navigationStore.StatusBarViewModel;

        #endregion

        #region Constructor
        public LayoutViewModel(INavigationStore navigationStore, INavigationBarViewModel navigationBarViewModel, IHomeViewModel homeViewModel, IStatusBarViewModel statusBarViewModel)
        {
            _navigationStore = navigationStore;
            _navigationStore.ContentScreen = homeViewModel;
            _navigationStore.MainContentViewModel = navigationBarViewModel;
            _navigationStore.StatusBarViewModel = statusBarViewModel;
            _navigationStore.ContentViewModelChanged += OnContentViewModelChanged;
            _navigationStore.MainContentViewModelChanged += OnMainContentViewModelChanged;
        }
        #endregion

        #region Methods


        private void OnMainContentViewModelChanged()
        {
            OnProperychanged(nameof(MainContentViewModel));
        }

        private void OnStatusBarViewModelChanged()
        {
            OnProperychanged(nameof(StatusBarViewModel));
        }

        private void OnContentViewModelChanged()
        {
            OnProperychanged(nameof(contentViewModel));
        }
        #endregion
    }
}
