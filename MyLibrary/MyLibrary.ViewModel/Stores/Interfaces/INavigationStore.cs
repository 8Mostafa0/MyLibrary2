using MyLibrary.ViewModel.ViewModels.Interfaces;
using System;

namespace MyLibrary.ViewModel.Stores.Interfaces
{
    public interface INavigationStore
    {
        IViewModelBase ContentScreen { get; set; }
        INavigationBarViewModel MainContentViewModel { get; set; }
        IStatusBarViewModel StatusBarViewModel { get; set; }

        event Action ContentViewModelChanged;
        event Action MainContentViewModelChanged;
        event Action StatusBarViewModelChanged;
    }
}
