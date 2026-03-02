using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class EditClientCommand : CommandBase
    {
        #region Dependencies
        private IClientsStore _clientsStore;
        private IClientsViewModel _clientsViewModel;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        ///  validate selected client then input values the edite client using clients store
        /// </summary>
        /// <param name="clientsViewModel"></param>
        /// <param name="clientsStore"></param>
        public EditClientCommand(IClientsViewModel clientsViewModel, IClientsStore clientsStore, IMessageBoxStore messageBoxStore)
        {
            _clientsViewModel = clientsViewModel;
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
            Client client = _clientsViewModel.SelectedClient;
            if (client == null)
            {
                _messageBoxStore.Show("لطفا کاربری را برای ویرایش انتخاب کنید", "ویرایش کاربر");
                return;
            }
            if (string.IsNullOrEmpty(_clientsViewModel.FirstName))
            {
                _messageBoxStore.Show("لطفا ابتدا نام را وارد کنید", "ویرایش کاربر");
                return;
            }
            if (string.IsNullOrEmpty(_clientsViewModel.LastName))
            {
                _messageBoxStore.Show("لطفا ابتدا فامیلی را وارد کنید", "ویرایش کاربر");
                return;
            }
            client.FirstName = _clientsViewModel.FirstName;
            client.LastName = _clientsViewModel.LastName;
            client.Tier = _clientsViewModel.Tier;
            await _clientsStore.EditClient(client);
        }
        #endregion
    }
}
