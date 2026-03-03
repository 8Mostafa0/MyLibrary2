using MyLibrary.ViewModel.Factory;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class LoadReservedBooksCommand : CommandBase, ILoadReservedBooksCommand
    {
        #region Dependencies
        private IReservedBooksStore _reservedBooksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load all reservations from ReserveBooks table to reserved books store
        /// </summary>
        /// <param name="reservedBooksStore"></param>
        public LoadReservedBooksCommand()
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
            await _reservedBooksStore.Load();
        }
        #endregion
    }
}
