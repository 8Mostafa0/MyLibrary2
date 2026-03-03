using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        #region Dependencies
        private readonly ILayoutViewModel _layoutViewModel;
        private IModalNavigationStore _modalNavigationStore;
        private IMessageBoxStore _messageBoxStore;

        public IViewModelBase CurrentMessageBox => _messageBoxStore.MessageBoxViewModel;
        public IViewModelBase CurrentViewModel => _layoutViewModel;
        public IViewModelBase CurrentModalView => _modalNavigationStore.CurrentViewModel;

        public bool IsModalOpen => _modalNavigationStore.IsModalOpen;

        public bool IsMessageBoxOpen => _messageBoxStore.IsMessageOpen;
        #endregion
        #region Constructor
        public MainViewModel()
        {
            _layoutViewModel = ClassFactory.CreateLayoutViewModel();
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            _modalNavigationStore.CurrentViewModelChanged += OnModalChanged;
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
            _messageBoxStore.MessageViewModelChanged += OnMessageBoxChanged;
            ClassFactory.CreateLoginModalCommand().Execute(null);

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
