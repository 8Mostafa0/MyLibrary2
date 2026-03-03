using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class LoadClientsCommand : CommandBase, ILoadClientsCommand
    {
        #region Dependencies
        private readonly IClientsStore _clientsStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load clients from database to clients store
        /// </summary>
        /// <param name="clientsStore"></param>
        public LoadClientsCommand()
        {
            _clientsStore = ClassFactory.CreateClientsStore();
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
