using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class ReloadClientsCommand : CommandBase, IReloadClientsCommand
    {
        #region Dependencies
        private readonly IClientsStore _clientsStore;
        private readonly IClientsViewModel _clientsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// reload clients list in clients store
        /// </summary>
        /// <param name="clientsStore"></param>
        /// <param name="clientsViewModel"></param>
        public ReloadClientsCommand()
        {
            _clientsStore = ClassFactory.CreateClientsStore();
            _clientsViewModel = ClassFactory.CreateClientsViewModel();
        }
        #endregion

        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            await _clientsStore.GetOrderedClients();

        }
        #endregion
    }
}
