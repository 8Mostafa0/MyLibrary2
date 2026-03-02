using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public class ReservedBooksStore : IReservedBooksStore
    {
        #region Dependencies
        private ReservedBooksRepository _resrvedBooksRepository;
        private List<ReservedBook> _reservedBooks;

        public IEnumerable<ReservedBook> ReservedBook => _reservedBooks;
        public Lazy<Task> _initilizeLazy;
        public Action ReseredBooksUpdated;
        public event Action<ReservedBook> ReservBookAdded;
        public event Action<ReservedBook> ReservBookEdited;
        public event Action<ReservedBook> ReservBookDeleted;

        #endregion

        #region Contructor
        /// <summary>
        /// 
        /// </summary>
        public ReservedBooksStore()
        {
            _reservedBooks = new List<ReservedBook>();
            _initilizeLazy = new Lazy<Task>(Initilize);
            _resrvedBooksRepository = new ReservedBooksRepository();
        }
        #endregion

        #region Methods
        /// <summary>
        /// load all book for first time to  reservedbooks list
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            await _initilizeLazy.Value;
        }

        /// <summary>
        /// call reservedbook update method of databse and invoke reservedbook update event
        /// </summary>
        /// <param name="reservedBook"></param>
        /// <returns></returns>
        public async Task UpdateReservedBook(ReservedBook reservedBook)
        {
            await _resrvedBooksRepository.EditReservBookToDb(reservedBook);
            ReservBookEdited?.Invoke(reservedBook);
        }
        /// <summary>
        /// call add new reservedbook method of database and invoe add new reservedbook event
        /// </summary>
        /// <param name="reservedBook"></param>
        /// <returns></returns>
        public async Task AddReservBook(ReservedBook reservedBook)
        {
            await _resrvedBooksRepository.AddNewReservedBookToDb(reservedBook);
            ReservBookAdded?.Invoke(reservedBook);
        }

        /// <summary>
        /// call delete reservedbook method of database and invoke delete reservedbook evnet
        /// </summary>
        /// <param name="reservedBook"></param>
        /// <returns></returns>
        public async Task DeleteReservBook(ReservedBook reservedBook)
        {
            await _resrvedBooksRepository.DeleteReservedBookWithClientToDb(reservedBook);
            ReservBookDeleted?.Invoke(reservedBook);
        }

        /// <summary>
        /// clear reservedbook list in store 
        /// </summary>
        public void Clear()
        {
            _reservedBooks.Clear();
        }

        /// <summary>
        /// get all selected reservedbook using sql query from database and store them in the memory
        /// </summary>
        /// <param name="customSql"></param>
        /// <returns></returns>
        public async Task GetReservedBooksAsync(string customSql = "")
        {
            IEnumerable<ReservedBook> reservedBooks = await _resrvedBooksRepository.GetAllReservedBooks(customSql);
            _reservedBooks.Clear();
            _reservedBooks.AddRange(reservedBooks);
            ReseredBooksUpdated?.Invoke();
        }

        /// <summary>
        /// get all reserved books ad save them inside memory for first time and then use memory values for next calls
        /// </summary>
        /// <returns></returns>
        private async Task Initilize()
        {
            IEnumerable<ReservedBook> reservedBooks = await _resrvedBooksRepository.GetAllReservedBooks();
            _reservedBooks.Clear();
            _reservedBooks.AddRange(reservedBooks);
            ReseredBooksUpdated?.Invoke();
        }
        #endregion
    }
}
