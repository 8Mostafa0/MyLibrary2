using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
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