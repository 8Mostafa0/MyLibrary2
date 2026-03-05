using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands
{
    public class NavigateHomeScreenCommand : CommandBase, INavigateHomeScreenCommand
    {
        #region Dependencies
        private readonly ILoansStore _loansStore;
        private readonly IBooksStore _booksStore;
        private readonly IClientsStore _clientsStore;
        private readonly INavigationStore _navigationStore;
        private readonly ISettingsStore _settingsStore;
        private readonly IBooksViewModel _bookApiViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of main navigation to home view 
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="settingsStore"></param>
        /// <param name="navigationStore"></param>
        public NavigateHomeScreenCommand(
            ILoansStore loansStore,
            IBooksStore booksStore,
            IClientsStore clientsStore,
            ISettingsStore settingsStore,
            INavigationStore navigationStore,
            IBooksViewModel bookApiViewModel
            )
        {
            _booksStore = booksStore;
            _loansStore = loansStore;
            _clientsStore = clientsStore;
            _settingsStore = settingsStore;
            _navigationStore = navigationStore;
            _bookApiViewModel = bookApiViewModel;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            await _clientsStore.Load();
            await _booksStore.Load();
            await _loansStore.Load();
            _navigationStore.ContentScreen = new HomeViewModel(_booksStore, _loansStore, _settingsStore, _clientsStore, _bookApiViewModel);
        }
        #endregion
    }
}
