using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels.LoanViewModels;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ShowLoanModalCommand : CommandBase, IShowLoanModalCommand
    {
        #region Dependencies
        private IModalNavigationStore _modalNavigationStore;
        private IAddEditeLoanViewModel _addEditeLoanViewModel;
        #endregion


        #region Contructor
        /// <summary>
        /// show loan modal by set modal view to the loan modal
        /// </summary>
        public ShowLoanModalCommand()
        {
            _modalNavigationStore = ClassFactory.CreateModalNavigationStore();
            _addEditeLoanViewModel = ClassFactory.CreateAddEditeLoanViewModel();
        }
        #endregion


        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _addEditeLoanViewModel.LoadClientsCommand.Execute(null);
            _modalNavigationStore.CurrentViewModel = _addEditeLoanViewModel;
        }
        #endregion
    }
}
