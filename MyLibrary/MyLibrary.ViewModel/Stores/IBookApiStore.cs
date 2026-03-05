using MyLibrary.Model.Models;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public interface IBookApiStore
    {
        NewBook BookData { get; set; }

        event Action BookDataChanged;
    }
}