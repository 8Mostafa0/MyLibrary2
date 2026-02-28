using MyLibrary.ViewModel.ViewModels.Interfaces;

namespace MyLibrary.ViewModel.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region Dependencies
        private readonly IViewModelBase _layoutViewModel;
        private IModalNavigationStore _modalNavigationStore;
        public IViewModelBase CurrentViewModel => _layoutViewModel;
        public IViewModelBase CurrentModalView => _modalNavigationStore.CurrentViewModel;

        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;
        #endregion
        #region Constructor
        public MainViewModel(ILayoutViewModel layoutViewModel, IModalNavigationStore modalNavigationStore, ILoanRepository loanRepository, IDbContextFactory dbContextFactory, ISettingsStore settingsStore)
        {
            _layoutViewModel = layoutViewModel;
            _modalNavigationStore = modalNavigationStore;
            _modalNavigationStore.CurrentViewModelChanged += OnModalChanged;
            new LoginModalCommand(_modalNavigationStore, loanRepository, dbContextFactory, settingsStore).Execute(null);

        }
        #endregion
        #region Methods

        /// <summary>
        /// get called each tim change value of modal navigation event trigred
        /// </summary>
        private void OnModalChanged()
        {
            OnProperychanged(nameof(CurrentModalView));
            OnProperychanged(nameof(IsModalOpen));
        }

        #endregion
    }
}
