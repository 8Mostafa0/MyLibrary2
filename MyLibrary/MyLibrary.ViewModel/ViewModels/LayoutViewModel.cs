namespace MyLibrary.ViewModel.ViewModels
{
    public class LayoutViewModel : ViewModelBase
    {
        #region Dependencies
        private readonly NavigationStore _navigationStore;
        public ViewModelBase contentViewModel => _navigationStore.ContentScreen;
        public ViewModelBase MainContentViewModel => _navigationStore.MainContentViewModel;

        public ViewModelBase StatusBarViewModel => _navigationStore.StatusBarViewModel;

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
