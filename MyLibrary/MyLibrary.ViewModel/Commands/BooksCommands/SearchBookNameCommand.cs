using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class SearchBookNameCommand : CommandBase
    {
        #region Dependencies
        private BooksStore _booksStore;
        #endregion


        #region Contructor
        /// <summary>
        /// Search Book Using Name From BookStore.
        /// </summary>
        /// <param name="booksStore"></param>
        public SearchBookNameCommand(BooksStore booksStore)
        {
            _booksStore = booksStore;
        }
        #endregion


        #region Execution
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override async void Execute(object? parameter)
        {
            string SearchSql = $"SELECT * FROM Books WHERE Name LIKE N'%{_booksStore.SearchBookName}%'";
            await _booksStore.GetAllBooks(SearchSql);
        }
        #endregion
    }
