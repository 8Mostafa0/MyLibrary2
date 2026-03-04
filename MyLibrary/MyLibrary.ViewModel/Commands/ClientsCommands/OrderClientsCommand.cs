using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class OrderClientsCommand : CommandBase, IOrderClientsCommand
    {
        #region Dependencies
        private readonly IClientsStore _clitentsStore;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// order clients based on tier
        /// </summary>
        /// <param name="clientsStore"></param>
        public OrderClientsCommand(IClientsStore clientsStore)
        {
            _clitentsStore = clientsStore;
        }
        #endregion

        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            string customSql = $"SELECT * FROM Clients WHERE Tier = '{_clitentsStore.SortOrder}'";
            await _clitentsStore.GetOrderedClients(customSql);
        }
        #endregion
    }
}
