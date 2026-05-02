using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyLibrary.Model.Base
{
    public class PropertyChangedBase : IPropertyChangedBase, INotifyPropertyChanged
    {
        #region EVENTS

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region METHODS

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void SetProperty<T>(ref T field, T value, Action eventAction)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                eventAction?.Invoke();
            }
        }

        #endregion

        #region IDisposable Implementation

        public virtual void Dispose() { }

        #endregion
    }
}
