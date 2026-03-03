using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class ReloadLoansListCommand : CommandBase, IReloadLoansListCommand
    {
        #region Dependencies
        private ILoansStore _loansStore;
        #endregion


        #region Contructor
        /// <summary>
        ///  reload all loans from database to loans store
        /// </summary>
        /// <param name="loansStore"></param>
        public ReloadLoansListCommand()
        {
            _loansStore = ClassFactory.CreateLoansStore();
        }
        #endregion

        #region Execution

        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            await _loansStore.GetAllLoans();
        }
        #endregion
    }
}

