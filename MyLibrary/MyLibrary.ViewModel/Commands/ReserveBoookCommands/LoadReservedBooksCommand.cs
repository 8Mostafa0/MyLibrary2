using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Commands.ReserveBoookCommands
{
    public class LoadReservedBooksCommand : CommandBase
    {
        #region Dependencies
        private ReservedBooksStore _reservedBooksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// load all reservations from ReserveBooks table to reserved books store
        /// </summary>
        /// <param name="reservedBooksStore"></param>
        public LoadReservedBooksCommand(ReservedBooksStore reservedBooksStore)
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
            await _reservedBooksStore.Load();
        }
        #endregion
    }
}
