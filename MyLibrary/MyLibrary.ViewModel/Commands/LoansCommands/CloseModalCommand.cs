using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.LoansCommands
{
    public class CloseModalCommand : CommandBase
    {
        #region Dependencies
        private ModalNavigationStore _modalNavigationStore;
        #endregion

        #region Contructor
        /// <summary>
        ///  close modal by set current view model to null
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        public CloseModalCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _modalNavigationStore.Close();
        }
        #endregion
    }
}
