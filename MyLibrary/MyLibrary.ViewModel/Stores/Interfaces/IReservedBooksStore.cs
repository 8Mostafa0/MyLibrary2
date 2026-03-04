using MyLibrary.Model.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyLibrary.ViewModel.Stores
{
    public interface IReservedBooksStore
    {
        IEnumerable<ReservedBook> ReservedBook { get; }

        event Action ReseredBooksUpdated;
        event Action<ReservedBook> ReservBookAdded;
        event Action<ReservedBook> ReservBookDeleted;
        event Action<ReservedBook> ReservBookEdited;

        ReservedBook SelectedReserv { get; set; }
        Client SelectedClient { get; set; }
        Book SelectedBook { get; set; }
        Task AddReservBook(ReservedBook reservedBook);
        void Clear();
        Task DeleteReservBook(ReservedBook reservedBook);
        Task GetReservedBooksAsync(string customSql = "");
        Task Load();
        Task UpdateReservedBook(ReservedBook reservedBook);
    }
}