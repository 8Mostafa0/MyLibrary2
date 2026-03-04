using MyLibrary.ViewModel.Stores;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore"></param>
        public LayoutViewModel(INavigationStore navigationStore, INavigationBarViewModel navigationBarViewModel)
        {
            _navigationStore = navigationStore;
            navigationStore.MainContentViewModel = navigationBarViewModel;
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
