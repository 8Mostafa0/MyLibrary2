using MyLibrary.ViewModel.Commands.MessageBoxCommands;
using MyLibrary.ViewModel.Stores;
using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.MessageBoxViewModel
{
    public class MessageBoxViewModel : ViewModelBase, IMessageBoxViewModel
    {
        #region Dependencies
        private string _title;
        private string _caption;
        private string _firstBtText;
        private string _secondBtText;
        private MessageBoxStore _messageBoxStor;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
            }
        }
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
            }
        }
        public string FirstBtText
        {
            get => _firstBtText;
            set
            {
                _firstBtText = value;
            }
        }
        public string SecondBtText
        {
            get => _secondBtText;
            set
            {
                _secondBtText = value;
            }
        }
        public string ShowSecondButton { get; set; }
        #endregion
        #region Commands
        public ICommand FirstBtCommand { get; }
        public ICommand SecondBtCommand { get; }
        #endregion
        #region Constructor
        public MessageBoxViewModel(MessageBoxStore messageBoxStore, string title, string caption, string firstBtText = null, string secondBtTetxt = null, ICommand command = null)
        {
            _messageBoxStor = messageBoxStore;
            Title = title;
            Caption = caption;
            FirstBtCommand = new MessageBoxConfrimCommand(_messageBoxStor, command);
            if (firstBtText is null)
            {
                FirstBtText = "تایید";
            }
            else
            {
                FirstBtText = firstBtText;
            }

            if (!(secondBtTetxt is null))
            {
                SecondBtText = secondBtTetxt;
                SecondBtCommand = new CloseMessageBox(_messageBoxStor);
                ShowSecondButton = "Visible";
            }
            else
            {
                ShowSecondButton = "Hidden";
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
