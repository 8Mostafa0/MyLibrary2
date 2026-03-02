namespace MyLibrary.ViewModel.ViewModels.SettingsViewModels
{
    public interface ILoanSettingsViewModel : IViewModelBase
    {
        string MaxBooksCount { get; set; }
        string MaxLoanDay { get; set; }

        void SettingsChanged();
    }
}