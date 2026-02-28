using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class ResetReservBookCommand : CommandBase
    {
        #region Dependencies
        private ReservedBooksStore _reservedBooksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load all reserved book from database to reserved book store
        /// </summary>
        /// <param name="reservedBooksStore"></param>
        public ResetReservBookCommand(ReservedBooksStore reservedBooksStore)
        {
            _reservedBooksStore = reservedBooksStore;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object? parameter)
        {
            await _reservedBooksStore.GetReservedBooksAsync();
        }
        #endregion
    }
}
