using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class DeleteClientCommand : CommandBase, IDeleteClientCommand
    {
        #region Dependencies
        private ILoanRepository _loanRepository;
        private readonly IClientsStore _clientsStore;
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
        /// <param name="clientsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        /// <param name="messageBoxStore"></param>
        public DeleteClientCommand(
            IClientsStore clientsStore,
            ILoanRepository loanRepository,
            IMessageBoxStore messageBoxStore,
            IReservedBooksRepository reservedBooksRepository
            )
        {
            _clientsStore = clientsStore;
            _loanRepository = loanRepository;
            _messageBoxStore = messageBoxStore;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public override async void Execute(object parameter)
        {
            if (_clientsStore.SelectedClient is null)
            {
                _messageBoxStore.Show("لطفا ابتدا کاربری را برای حذف انتخاب کنید", "حذف کاربر");
                return;
            }
            List<Loan> UserLoans = await _loanRepository.GetAllClientLoans(_clientsStore.SelectedClient.ID);
            if (UserLoans.Count > 0)
            {
                _messageBoxStore.Show("این کاربر امانتی تحویل نشده دارد", "حذف کاربر");
                return;
            }
            else
            {

                _messageBoxStore.Show("کاربر حذف شود؟", "حذف کاربر", "بله", "خیر", this);
                if (_messageBoxStore.MessageBoxResult)
                {
                    _messageBoxStore.CloseMessageBox();
                    await _reservedBooksRepository.RemoveClientReservedBooks(_clientsStore.SelectedClient.ID);
                    await _loanRepository.RemoveClientLoans(_clientsStore.SelectedClient.ID);
                    await _clientsStore.DeleteClient(_clientsStore.SelectedClient);
                }
            }
        }
        #endregion
    }
}
