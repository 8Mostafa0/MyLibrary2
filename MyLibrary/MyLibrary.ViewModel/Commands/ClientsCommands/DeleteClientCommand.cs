using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Factory;
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
        /// Check For Selected Client And If Clients Not Have Not Returned Loan Then :
        /// 1_ Remove All Reservations of Client
        /// 2_ Remove All Saved Loans Of Client
        /// 3_ Delete Client Using Clients Store
        /// </summary>
        public DeleteClientCommand()
        {
            _clientsViewModel = ClassFactory.CreateClientsViewModel();
            _clientsStore = ClassFactory.CreateClientsStore();
            _loanRepository = ClassFactory.CreateLoanRepository();
            _reservedBooksRepository = ClassFactory.CreateReservedBooksRepository();
            _messageBoxStore = ClassFactory.CreateMessageBoxStore();
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

                _messageBoxStore.Show("کاربر حذف شود؟", "حذف کاربر", "بله", "خیر", new DeleteClientCommand(_clientsViewModel, _clientsStore, _loanRepository, _reservedBooksRepository, _messageBoxStore));
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
