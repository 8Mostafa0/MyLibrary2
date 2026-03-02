using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.MessageBoxViewModel
{
    public interface IMessageBoxViewModel : IViewModelBase
    {
        string Caption { get; set; }
        ICommand FirstBtCommand { get; }
        string FirstBtText { get; set; }
        ICommand SecondBtCommand { get; }
        string SecondBtText { get; set; }
        string ShowSecondButton { get; set; }
        string Title { get; set; }
    }
}