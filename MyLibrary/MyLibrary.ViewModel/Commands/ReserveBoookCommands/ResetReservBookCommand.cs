using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class ResetReservBookCommand : CommandBase, IResetReservBookCommand
    {
        #region Dependencies
        private IReservedBooksStore _reservedBooksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load all reserved book from database to reserved book store
        /// </summary>
        public ResetReservBookCommand()
        {
            _reservedBooksStore = ClassFactory.CreateReservedBooksStore();
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object parameter)
        {
            await _reservedBooksStore.GetReservedBooksAsync();
        }
        #endregion
    }
}
