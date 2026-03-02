using MyLibrary.Model.DbContexts;
using MyLibrary.ViewModel.Commands;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Dependencies
        private readonly LayoutViewModel _layoutViewModel;
        private ModalNavigationStore _modalNavigationStore;
        private MessageBoxStore _messageBoxStore;

        public ViewModelBase CurrentMessageBox => _messageBoxStore.MessageBoxViewModel;
        public ViewModelBase CurrentViewModel => _layoutViewModel;
        public ViewModelBase CurrentModalView => _modalNavigationStore.CurrentViewModel;

        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;

        public bool IsMessageBoxOpen => _messageBoxStore.IsMessageOpen;
        #endregion
        #region Constructor
        public MainViewModel(LayoutViewModel layoutViewModel, ModalNavigationStore modalNavigationStore, MessageBoxStore messageBoxStore)
        {
            _layoutViewModel = layoutViewModel;
            _modalNavigationStore = modalNavigationStore;
            _modalNavigationStore.CurrentViewModelChanged += OnModalChanged;
            _messageBoxStore = messageBoxStore;
            _messageBoxStore.MessageViewModelChanged += OnMessageBoxChanged;
            new LoginModalCommand(_modalNavigationStore, new DbContextFactory(), new SettingsStore(), messageBoxStore).Execute(null);

        }
        #endregion
        #region Methods

        /// <summary>
        /// get called each tim change value of modal navigation event trigred
        /// </summary>
        private void OnModalChanged()
        {
            OnProperychanged(nameof(CurrentViewModel));
            OnProperychanged(nameof(IsModalOpen));
        }


        /// <summary>
        /// get called each tim change value of Messagebox Changed event trigred
        /// </summary>
        private void OnMessageBoxChanged()
        {

            OnProperychanged(nameof(CurrentMessageBox));
            OnProperychanged(nameof(IsMessageBoxOpen));
        }

        #endregion
    }
}
