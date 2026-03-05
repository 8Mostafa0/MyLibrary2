using MyLibrary.ViewModel.Commands.BookApiCommands;
using MyLibrary.ViewModel.Services.BookApi;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.ViewModels
{
    public class BookApiViewModel : ViewModelBase, IBookApiViewModel
    {
        #region Dependencies
        private readonly IBookApiStore _bookApiStore;
        public int ID => _bookApiStore.BookData.ID;
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
            _bookApiStore.BookDataChanged += OnBookDataChanged;
            NextBookCommand.Execute(null);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Change data on each response of reqeusts
        /// </summary>
        private void OnBookDataChanged()
        {
            OnProperychanged(nameof(ID));
            OnProperychanged(nameof(Name));
            OnProperychanged(nameof(Author));
            OnProperychanged(nameof(Description));
        }
        #endregion

    }
}
