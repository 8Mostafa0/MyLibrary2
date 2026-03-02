using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        #region Dependencies
        private readonly NavigationStore _navigationStore;
        public IViewModelBase contentViewModel => _navigationStore.ContentScreen;
        public IViewModelBase MainContentViewModel => _navigationStore.MainContentViewModel;

        public IStatusBarViewModel StatusBarViewModel => _navigationStore.StatusBarViewModel;

        #endregion

        #region Constructor
        public LayoutViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.ContentViewModelChanged += OnContentViewModelChanged;
            _navigationStore.MainContentViewModelChanged += OnMainContentViewModelChanged;
        }
        #endregion

        #region Methods


        private void OnMainContentViewModelChanged()
        {
            OnProperychanged(nameof(MainContentViewModel));
        }

        private void OnIStatusBarViewModelChanged()
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
