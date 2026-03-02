using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public interface IModalNavigationStore
    {
        IViewModelBase CurrentViewModel { get; set; }
        bool IsModalOpen { get; }

        event Action CurrentViewModelChanged;

        void Close();
    }
}