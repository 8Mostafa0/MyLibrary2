using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class NavigateLoansCommand : CommandBase
    {
        #region Dependencies
        private LoansViewModel _loansViewModel;
        private NavigationStore _navigationStore;
        #endregion


        #region Contructor
        /// <summary>
        /// set content of navigate to loans view model
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="modalNavigationStore"></param>
        /// <param name="loansStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="booksStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="settingsStore"></param>
        /// <param name="booksRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public NavigateLoansCommand(NavigationStore navigationStore, ModalNavigationStore modalNavigationStore, ILoansStore loansStore, IClientsStore clientsStore, IBooksStore booksStore, LoanRepository loanRepository, SettingsStore settingsStore, BooksRepository booksRepository, ReservedBooksRepository reservedBooksRepository, IMessageBoxStore messageBoxStore)
        {
            _navigationStore = navigationStore;
            //_loansViewModel = LoansViewModel.LoadViewModel(modalNavigationStore, loansStore, clientsStore, booksStore, loanRepository, settingsStore, booksRepository, reservedBooksRepository, messageBoxStore);
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _navigationStore.ContentScreen = _loansViewModel;
        }
        #endregion
    }
}
