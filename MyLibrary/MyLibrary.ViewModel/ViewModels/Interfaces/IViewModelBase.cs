using System.ComponentModel;

namespace MyLibrary.ViewModel.ViewModels.Interfaces
{
    public interface IViewModelBase
    {
        event PropertyChangedEventHandler PropertyChanged;
        void Dispose();
    }
}