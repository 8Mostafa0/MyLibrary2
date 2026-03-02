using System.ComponentModel;

namespace MyLibrary.ViewModel.ViewModels
{
    public interface IViewModelBase
    {
        event PropertyChangedEventHandler PropertyChanged;

        void Dispose();
    }
}