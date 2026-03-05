using MyLibrary.ViewModel.Services.BookApi;
using MyLibrary.ViewModel.Stores;

namespace MyLibrary.ViewModel.Commands.BookApiCommands
{
    public class NextBookCommand : CommandBase, INextBookCommand
    {
        #region Dependencies
        public IBookApi _bookApi;
        private ISettingsStore _settingsStore;
        #endregion
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookApi"></param>
        /// <param name="settingsStore"></param>
        public NextBookCommand(IBookApi bookApi, ISettingsStore settingsStore)
        {
            _bookApi = bookApi;
            _settingsStore = settingsStore;
        }
        #endregion
        #region Methods
        public override void Execute(object parameter)
        {
            int currentBookId = _settingsStore.GetBookApiCounter();
            currentBookId++;
            _bookApi.GetBookInfo(currentBookId);
            _settingsStore.SaveBookApiCounter(currentBookId);
        }
        #endregion
    }
}
