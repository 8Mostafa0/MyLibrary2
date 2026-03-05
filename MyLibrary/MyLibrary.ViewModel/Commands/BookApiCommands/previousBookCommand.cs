using MyLibrary.ViewModel.Services.BookApi;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BookApiCommands
{
    public class previousBookCommand : CommandBase, IpreviousBookCommand
    {
        public IBookApi _bookApi;
        private ISettingsStore _settingsStore;
        private IMessageBoxStore _messageBoxStore;
        public previousBookCommand(IBookApi bookApi, ISettingsStore settingsStore, IMessageBoxStore messageBoxStore)
        {
            _bookApi = bookApi;
            _settingsStore = settingsStore;
            _messageBoxStore = messageBoxStore;
        }
        public override void Execute(object parameter)
        {
            int currentBookId = _settingsStore.GetBookApiCounter();
            if (currentBookId == 1)
            {
                _messageBoxStore.Show("شما به اولین کتاب رسیدید لطفا کتاب های بعدی رو مشاهده کنید", "کتاب قبلی");
                return;
            }
            currentBookId--;
            _bookApi.GetBookInfo(currentBookId);
            _settingsStore.SaveBookApiCounter(currentBookId);
        }
    }
}
