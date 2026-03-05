using MyLibrary.ViewModel.Services.BookApi;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BookApiCommands
{
    public class NextBookCommand : CommandBase, INextBookCommand
    {
        public IBookApi _bookApi;
        private ISettingsStore _settingsStore;
        public NextBookCommand(IBookApi bookApi, ISettingsStore settingsStore)
        {
            _bookApi = bookApi;
            _settingsStore = settingsStore;
        }
        public override void Execute(object parameter)
        {
            int currentBookId = _settingsStore.GetBookApiCounter();
            currentBookId++;
            _bookApi.GetBookInfo(currentBookId);
            _settingsStore.SaveBookApiCounter(currentBookId);
        }
    }
}
