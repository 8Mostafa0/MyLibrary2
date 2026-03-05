using MyLibrary.Model.Models;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public interface IBookApiStore
    {
        Book BookData { get; set; }

        event Action BookDataChanged;
    }
}