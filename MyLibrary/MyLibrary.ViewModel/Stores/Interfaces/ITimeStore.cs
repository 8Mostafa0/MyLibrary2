using System;

namespace MyLibrary.ViewModel.Stores.Interfaces
{
    public interface ITimeStore
    {
        DateTime CurrentTime { get; set; }

        event Action CurrentViewModelChanged;
    }
}
