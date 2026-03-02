using MyLibrary.ViewModel.Factory;
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
        #endregion

        #region Contructor
        /// <summary>
        /// set current view of main navigation to home view 
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        public NavigateHomeScreenCommand()
        {
            _navigationStore = ClassFactory.CreateNavigationStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _booksStore = ClassFactory.CreateBooksStore();
            _loansStore = ClassFactory.CreateLoansStore();
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
            _navigationStore.ContentScreen = new HomeViewModel(_clientsStore, _booksStore, _loansStore);
        }
        #endregion
    }
}
