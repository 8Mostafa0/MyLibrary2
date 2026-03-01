using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.Commands.ClientsCommands
{
    public class DeleteClientCommand : CommandBase
    {
        #region Dependencies
        private LoanRepository _loanRepository;
        private readonly ClientsStore _clientsStore;
        private readonly ClientsViewModel _clientsViewModel;
        private ReservedBooksRepository _reservedBooksRepository;
        #endregion


        #region Constructor
        /// <summary>
        /// Check For Selected Client And If Clients Not Have Not Returned Loan Then :
        /// 1_ Remove All Reservations of Client
        /// 2_ Remove All Saved Loans Of Client
        /// 3_ Delete Client Using Clients Store
        /// </summary>
        /// <param name="clientsViewModel"></param>
        /// <param name="clientsStore"></param>
        /// <param name="loanRepository"></param>
        /// <param name="reservedBooksRepository"></param>
        public DeleteClientCommand(ClientsViewModel clientsViewModel, ClientsStore clientsStore, LoanRepository loanRepository, ReservedBooksRepository reservedBooksRepository)
        {
            _clientsViewModel = clientsViewModel;
            _clientsStore = clientsStore;
            _loanRepository = loanRepository;
            _reservedBooksRepository = reservedBooksRepository;
        }
        #endregion


        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public override async void Execute(object? parameter)
        {
            if (_clientsViewModel.SelectedClient is null)
            {
                MessageBox.Show("لطفا ابتدا کاربری را برای حذف انتخاب کنید", "حذف کاربر");
                return;
            }
            List<Loan> UserLoans = await _loanRepository.GetAllClientLoans(_clientsViewModel.SelectedClient.ID);
            if (UserLoans.Count > 0)
            {
                MessageBox.Show("این کاربر امانتی تحویل نشده دارد", "حذف کاربر");
                return;
            }
            else
            {
                MessageBoxResult AskToDelete = MessageBox.Show("کاربر حذف شود؟", "حذف کاربر", MessageBoxButton.YesNo);
                if (AskToDelete == MessageBoxResult.Yes)
                {
                    await _reservedBooksRepository.RemoveClientReservedBooks(_clientsViewModel.SelectedClient.ID);
                    await _loanRepository.RemoveClientLoans(_clientsViewModel.SelectedClient.ID);
                    await _clientsStore.DeleteClient(_clientsViewModel.SelectedClient);
                }
            }
        }
        #endregion
    }
}
