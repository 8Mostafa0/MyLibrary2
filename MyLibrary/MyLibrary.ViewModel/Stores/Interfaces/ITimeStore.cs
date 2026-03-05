using System;

namespace MyLibrary.ViewModel.Stores
{
    public interface ITimeStore
    {
        string CurrentTime { get; set; }

        event Action CurrentViewModelChanged;
    }
}