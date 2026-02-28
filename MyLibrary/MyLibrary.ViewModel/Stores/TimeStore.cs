using System;

namespace MyLibrary.ViewModel.Stores
{
    public class TimeStore
    {
        #region Dependencies
        public event Action CurrentViewModelChanged;

        private DateTime _currentTime;
        public DateTime CurrentTime
        {
            get => _currentTime;
            set
            {

                _currentTime = value;
                OnCurrentTimeChange();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// call to change view event
        /// </summary>
        private void OnCurrentTimeChange()
        {
            CurrentViewModelChanged?.Invoke();
        }

        #endregion

    }
}
