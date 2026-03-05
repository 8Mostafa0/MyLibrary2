using MyLibrary.Model.Models;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public class BookApiStore : IBookApiStore
    {
        #region Dependencies
        public event Action BookDataChanged;

        private Book _bookData;
        public Book BookData
        {
            get => _bookData;
            set
            {
                _bookData = value;
                OnBookDataChange();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Triger event of when data is changed to update the view
        /// </summary>
        private void OnBookDataChange()
        {
            BookDataChanged?.Invoke();
        }
        #endregion
    }
}
