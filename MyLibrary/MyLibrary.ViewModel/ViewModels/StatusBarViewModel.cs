using MyLibrary.ViewModel.Stores;
using System;
using System.Timers;

namespace MyLibrary.ViewModel.ViewModels
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        #region Dependencies
        private static System.Timers.Timer aTimer;
        private ITimeStore _timeStore;
        public DateTime CloclString => _timeStore.CurrentTime;
        #endregion

        #region Constructor
        public StatusBarViewModel(ITimeStore timeStore)
        {
            _timeStore = timeStore;
            _timeStore.CurrentTime = DateTime.Now;
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;
        }
        #endregion

        #region Methods
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _timeStore.CurrentTime = DateTime.Now;
            OnProperychanged(nameof(CloclString));
        }
        #endregion
    }
}
