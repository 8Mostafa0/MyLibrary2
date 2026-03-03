using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class LoadLoansCommand : CommandBase, ILoadLoansCommand
    {
        #region Dependencies
        private ILoansStore _loansStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load all loans from database to loans store
        /// </summary>
        /// <param name="loansStore"></param>
        public LoadLoansCommand(ILoansStore loansStore)
        {
            _loansStore = loansStore;
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
