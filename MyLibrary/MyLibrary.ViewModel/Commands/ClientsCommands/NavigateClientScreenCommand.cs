using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class NavigateClientScreenCommand : CommandBase, INavigateClientScreenCommand
    {
        #region Dependencies
        private IClientsStore _clientsStore;
        private INavigationStore _navigationStore;
        private IClientsViewModel _clientsViewModel;

        #endregion

        #region Contructor
        /// <summary>
        /// set content of curent view in content store to clients view model
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public NavigateClientScreenCommand()
        {
            _navigationStore = ClassFactory.CreateNavigationStore();
            _clientsStore = ClassFactory.CreateClientsStore();
            _clientsViewModel = ClassFactory.CreateClientsViewModel();
        }
        #endregion


        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _navigationStore.ContentScreen = _clientsViewModel;
        }
        #endregion
    }
}
