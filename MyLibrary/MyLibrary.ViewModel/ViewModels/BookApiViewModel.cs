using MyLibrary.ViewModel.Commands.BookApiCommands;
using MyLibrary.ViewModel.Services.BookApi;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.ViewModels
{
    public class BookApiViewModel : ViewModelBase, IBookApiViewModel
    {
        #region Dependencies
        private readonly IBookApiStore _bookApiStore;
        public string Name => _bookApiStore.BookData.Name;
        public string Author => _bookApiStore.BookData.Publisher;
        public string Description => _bookApiStore.BookData.Desciption;
        public INextBookCommand NextBookCommand { get; }
        public IpreviousBookCommand PreviousBookCommand { get; }
        #endregion
        #region Constructor
        public BookApiViewModel(IBookApiStore bookApiStore, IBookApi bookApi, ISettingsStore settingsStore, IMessageBoxStore messageBoxStore)
        {
            _bookApiStore = bookApiStore;
            NextBookCommand = new NextBookCommand(bookApi, settingsStore);
            PreviousBookCommand = new previousBookCommand(bookApi, settingsStore, messageBoxStore);
            _bookApiStore.BookDataChanged += GetFirstRequest;
        }
        #endregion
        #region Methods
        /// <summary>
        /// get the first book data to show in the view when the view model is created
        /// </summary>
        private void GetFirstRequest()
        {
            NextBookCommand.Execute(null);
            OnProperychanged(nameof(Name));
            OnProperychanged(nameof(Author));
            OnProperychanged(nameof(Description));
        }
        #endregion

    }
}
