using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels.MessageBox
{
    public class MessageBox : ViewModelBase
    {
        #region Dependencies
        private string _title;
        private string _caption;
        private string _firstBtText;
        private string _secondBtText;
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
        public bool ShowFirstButton => FirstBtText != "";
        public bool ShowSecondButton => SecondBtText != "";
        #endregion
        #region Commands
        public ICommand FirstBtCommand { get; }
        public ICommand SecondBtCommand { get; }
        #endregion
        #region Constructor
        public MessageBox(string title, string caption, string firstBtText = null, ICommand firstBtCommand = null, string secondBtTetxt = null, ICommand secondBtCommand = null)
        {
            Title = title;
            Caption = caption;
            if (firstBtText is null)
            {
                FirstBtText = "تایید";
            }
            else
            {
                FirstBtText = firstBtText;
                FirstBtCommand = firstBtCommand;
            }

            if (!(secondBtTetxt is null))
            {
                SecondBtText = secondBtTetxt;

            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
