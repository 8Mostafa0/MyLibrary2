using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class OrderClientsCommand : CommandBase, IOrderClientsCommand
    {
        #region Dependencies
        private readonly IClientsStore _clitentsStore;
        private readonly IClientsViewModel _clientsViewModel;
        #endregion

        #region Contructor
        /// <summary>
        /// order clients based on tier
        /// </summary>
        public OrderClientsCommand(IClientsViewModel clientsViewModel)
        {
            _clitentsStore = ClassFactory.CreateClientsStore();
            _clientsViewModel = clientsViewModel;
        }
        #endregion

        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            string customSql = $"SELECT * FROM Clients WHERE Tier = '{_clientsViewModel.SortOrder}'";
            await _clitentsStore.GetOrderedClients(customSql);
        }
        #endregion
    }
}
