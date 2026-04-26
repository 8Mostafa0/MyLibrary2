using System.ComponentModel;

namespace MyLibrary.Model.Base
{
    public interface IPropertyChangedBase
    {
        event PropertyChangedEventHandler PropertyChanged;

        void Dispose();
    }
}