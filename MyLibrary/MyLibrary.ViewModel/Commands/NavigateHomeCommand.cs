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
        private readonly IHomeViewModel _homeViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of main navigation to home view 
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="homeViewModel"></param>
        public NavigateHomeScreenCommand(
            INavigationStore navigationStore,
            IClientsStore clientsStore,
            IBooksStore booksStore,
            ILoansStore loansStore,
            IHomeViewModel homeViewModel
            )
        {
            _navigationStore = navigationStore;
            _clientsStore = clientsStore;
            _booksStore = booksStore;
            _loansStore = loansStore;
            _homeViewModel = homeViewModel;
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
            _navigationStore.ContentScreen = _homeViewModel;
        }
        #endregion
    }
}
