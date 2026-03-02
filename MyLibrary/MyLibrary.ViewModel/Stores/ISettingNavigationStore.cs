using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public interface ISettingNavigationStore
    {
        IViewModelBase CurrentSettingViewModel { get; set; }

        event Action SettingViewModelChanged;

        void OnSettingViewModelChanged();
    }
}