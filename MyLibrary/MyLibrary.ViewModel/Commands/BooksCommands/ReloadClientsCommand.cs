using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class ReloadClientsCommand : CommandBase
    {
        #region Dependencies
        private BooksStore _booksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="booksStore"></param>
        public ReloadClientsCommand(BooksStore booksStore)
        {
            _booksStore = booksStore;
        }
        #endregion


        #region Execution

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">No Parameter Need</param>
        public override async void Execute(object? parameter)
        {
            await _booksStore.GetAllBooks();
        }
        #endregion
    }
}
