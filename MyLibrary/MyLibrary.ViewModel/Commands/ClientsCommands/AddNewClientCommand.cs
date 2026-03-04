using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using System;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class AddNewClientCommand : CommandBase, IAddNewClientCommand
    {
        #region Dependencies
        private readonly IClientsStore _clientStore;
        private IMessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// Checks To Validate Clients Data First Then Add New Client Using Clients Store
        /// </summary>
        /// <param name="messageBoxStore"></param>
        /// <param name="clientsStore"></param>
        public AddNewClientCommand(
            IMessageBoxStore messageBoxStore,
            IClientsStore clientsStore)
        {
            _clientStore = clientsStore;
            _messageBoxStore = messageBoxStore;
        }
        #endregion


        #region Execcution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">No Perameer Needed</param>
        public override async void Execute(object parameter)
        {
            if (string.IsNullOrEmpty(_clientStore.SelectedClient.FirstName))
            {
                _messageBoxStore.Show("لطفا ابتدا نام را وارد کنید", "افزودن کاربر");
                return;
            }
            if (string.IsNullOrEmpty(_clientStore.SelectedClient.LastName))
            {
                _messageBoxStore.Show("لطفا ابتدا فامیلی را وارد کنید", "افزودن کاربر");
                return;
            }
            Client client = new Client()
            {
                FirstName = _clientStore.SelectedClient.FirstName,
                LastName = _clientStore.SelectedClient.LastName,
                Tier = _clientStore.SelectedClient.Tier,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _clientStore.AddNewClient(client);

        }
        #endregion
    }
}
