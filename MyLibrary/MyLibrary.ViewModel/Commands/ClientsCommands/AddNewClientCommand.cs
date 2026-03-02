using MyLibrary.Model.Models;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class AddNewClientCommand : CommandBase
    {
        #region Dependencies
        private readonly ClientsStore _clientStore;
        private readonly IClientsViewModel _clientViewModel;
        private MessageBoxStore _messageBoxStore;
        #endregion

        #region Contructor
        /// <summary>
        /// Checks To Validate Clients Data First Then Add New Client Using Clients Store
        /// </summary>
        /// <param name="clientsViewModel"></param>
        /// <param name="clientStore"></param>
        public AddNewClientCommand(IClientsViewModel clientsViewModel, ClientsStore clientStore, MessageBoxStore messageBoxStore)
        {
            _clientStore = clientStore;
            _clientViewModel = clientsViewModel;
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
            if (string.IsNullOrEmpty(_clientViewModel.FirstName))
            {
                _messageBoxStore.Show("لطفا ابتدا نام را وارد کنید", "افزودن کاربر");
                return;
            }
            if (string.IsNullOrEmpty(_clientViewModel.LastName))
            {
                _messageBoxStore.Show("لطفا ابتدا فامیلی را وارد کنید", "افزودن کاربر");
                return;
            }
            Client client = new Client()
            {
                FirstName = _clientViewModel.FirstName,
                LastName = _clientViewModel.LastName,
                Tier = _clientViewModel.Tier,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            await _clientStore.AddNewClient(client);

        }
        #endregion
    }
}
