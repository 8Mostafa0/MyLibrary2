namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class ReloadClientsCommand : CommandBase
    {
        #region Dependencies
        private readonly ClientsStore _clientsStore;
        private readonly ClientsViewModel _clientsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// reload clients list in clients store
        /// </summary>
        /// <param name="clientsStore"></param>
        /// <param name="clientsViewModel"></param>
        public ReloadClientsCommand(ClientsStore clientsStore, ClientsViewModel clientsViewModel)
        {
            _clientsStore = clientsStore;
            _clientsViewModel = clientsViewModel;
        }
        #endregion

        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object? parameter)
        {
            await _clientsStore.GetOrderedClients();

        }
        #endregion
    }
}
