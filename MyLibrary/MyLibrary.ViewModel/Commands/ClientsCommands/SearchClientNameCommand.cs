using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class SearchClientNameCommand : CommandBase, ISearchClientNameCommand
    {
        #region Dependencies
        private IClientsStore _clientsStore;
        #endregion


        #region Contructor
        /// <summary>
        /// search in clients database base on first name and last name
        /// </summary>
        /// <param name="clientsStore"></param>
        public SearchClientNameCommand()
        {
            _clientsStore = ClassFactory.CreateClientsStore();
        }
        #endregion


        #region Execution


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            string Name = _clientsStore.SearchClientName;
            string SearchSql = $"SELECT * FROM Clients WHERE FirstName LIKE N'%{Name}%' OR LastName LIKE N'%{Name}%'";
            await _clientsStore.GetOrderedClients(SearchSql);
        }
        #endregion
    }
}
