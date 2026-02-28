using System.ComponentModel;

namespace MyLibrary.ViewModel.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Dependencies
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructor
        protected void OnProperychanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Methods

        public virtual void Dispose() { }
        #endregion
    }
}
