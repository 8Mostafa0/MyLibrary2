using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class ReloadClientsCommand : CommandBase, IReloadClientsCommand
    {
        #region Dependencies
        private readonly IClientsStore _clientsStore;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// reload clients list in clients store
        /// </summary>
        /// <param name="clientsStore"></param>
        public ReloadClientsCommand(IClientsStore clientsStore)
        {
            _clientsStore = clientsStore;
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
