using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class NavigateClientScreenCommand : CommandBase
    {
        #region Dependencies
        private ClientsStore _clientsStore;
        private NavigationStore _navigationStore;
        private ClientsViewModel ClientsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// set content of curent view in content store to clients view model
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public NavigateClientScreenCommand(NavigationStore navigationStore, ClientsStore clientsStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository)
        {
            _navigationStore = navigationStore;
            _clientsStore = clientsStore;
            ClientsViewModel = ClientsViewModel.LoadViewModel(_clientsStore, loanRepository, reservedBooksRepository);
        }
        #endregion


        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _navigationStore.ContentScreen = ClientsViewModel;
        }
        #endregion
    }
}
