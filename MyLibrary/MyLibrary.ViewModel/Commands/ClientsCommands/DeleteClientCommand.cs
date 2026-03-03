using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class DeleteClientCommand : CommandBase, IDeleteClientCommand
    {
        #region Dependencies
        private ILoanRepository _loanRepository;
        private readonly IClientsStore _clientsStore;
        private readonly IClientsViewModel _clientsViewModel;
        private IReservedBooksRepository _reservedBooksRepository;

        private IMessageBoxStore _messageBoxStore;

        #endregion


        #region Constructor
        /// <summary>
        /// 
        /// Check For Selected Client And If Clients Not Have Not Returned Loan Then :
        /// 1_ Remove All Reservations of Client
        /// 2_ Remove All Saved Loans Of Client
        /// 3_ Delete Client Using Clients Store
        /// </summary>
        /// <param name="clientsViewModel"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="messageBoxStore"></param>
        public DeleteClientCommand(
            IClientsViewModel clientsViewModel,
            IClientsStore clientsStore,
            ILoanRepository loanRepository,
            IReservedBooksRepository reservedBooksRepository,
            IMessageBoxStore messageBoxStore)
        {
            _clientsViewModel = clientsViewModel;
            _clientsStore = clientsStore;
            _loanRepository = loanRepository;
            _reservedBooksRepository = reservedBooksRepository;
            _messageBoxStore = messageBoxStore;
        }
        #endregion


        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public override async void Execute(object parameter)
        {
            if (_clientsViewModel.SelectedClient is null)
            {
                _messageBoxStore.Show("لطفا ابتدا کاربری را برای حذف انتخاب کنید", "حذف کاربر");
                return;
            }
            List<Loan> UserLoans = await _loanRepository.GetAllClientLoans(_clientsViewModel.SelectedClient.ID);
            if (UserLoans.Count > 0)
            {
                _messageBoxStore.Show("این کاربر امانتی تحویل نشده دارد", "حذف کاربر");
                return;
            }
            else
            {

                _messageBoxStore.Show("کاربر حذف شود؟", "حذف کاربر", "بله", "خیر", ClassFactory.CreateDeleteClientCommand(_clientsViewModel));
                if (_messageBoxStore.MessageBoxResult)
                {
                    _messageBoxStore.CloseMessageBox();
                    await _reservedBooksRepository.RemoveClientReservedBooks(_clientsViewModel.SelectedClient.ID);
                    await _loanRepository.RemoveClientLoans(_clientsViewModel.SelectedClient.ID);
                    await _clientsStore.DeleteClient(_clientsViewModel.SelectedClient);
                }
            }
        }
        #endregion
    }
}
