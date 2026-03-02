using System;

namespace MyLibrary.ViewModel.Stores
{
    public interface ITimeStore
    {
        DateTime CurrentTime { get; set; }

        event Action CurrentViewModelChanged;
    }
}