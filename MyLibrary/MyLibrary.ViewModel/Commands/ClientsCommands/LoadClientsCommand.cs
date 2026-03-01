using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class LoadClientsCommand : CommandBase
    {
        #region Dependencies
        private readonly ClientsStore _clientsStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load clients from database to clients store
        /// </summary>
        /// <param name="clientsStore"></param>
        public LoadClientsCommand(ClientsStore clientsStore)
        {
            _clientsStore = clientsStore;
        }

        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no parameters needed</param>
        public override async void Execute(object parameter)
        {
            await _clientsStore.GetOrderedClients();
        }
        #endregion
    }
}
