using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class EditClientCommand : CommandBase, IEditClientCommand
    {
        #region Dependencies
        private IClientsStore _clientsStore;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        ///  validate selected client then input values the edite client using clients store
        /// </summary>
        /// <param name="clientsStore"></param>
        /// <param name="messageBoxStore"></param>
        public EditClientCommand(
            IClientsStore clientsStore,
            IMessageBoxStore messageBoxStore)
        {
            _clientsStore = clientsStore;
            _messageBoxStore = messageBoxStore;
        }
        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no parametes needed</param>
        public override async void Execute(object parameter)
        {
            Client client = _clientsStore.SelectedClient;
            if (client == null)
            {
                _messageBoxStore.Show("لطفا کاربری را برای ویرایش انتخاب کنید", "ویرایش کاربر");
                return;
            }
            if (string.IsNullOrEmpty(_clientsStore.SelectedClient.FirstName))
            {
                _messageBoxStore.Show("لطفا ابتدا نام را وارد کنید", "ویرایش کاربر");
                return;
            }
            if (string.IsNullOrEmpty(_clientsStore.SelectedClient.LastName))
            {
                _messageBoxStore.Show("لطفا ابتدا فامیلی را وارد کنید", "ویرایش کاربر");
                return;
            }
            client.FirstName = _clientsStore.SelectedClient.FirstName;
            client.LastName = _clientsStore.SelectedClient.LastName;
            client.Tier = _clientsStore.SelectedClient.Tier;
            await _clientsStore.EditClient(client);
        }
        #endregion
    }
}
