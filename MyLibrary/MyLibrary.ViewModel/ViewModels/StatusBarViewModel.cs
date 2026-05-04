using MyLibrary.ViewModel.Stores;
using System;
using System.Globalization;
using System.Timers;

namespace MyLibrary.ViewModel.ViewModels
{
    public class StatusBarViewModel : ViewModelBase, IStatusBarViewModel
    {
        #region Dependencies
        private static System.Timers.Timer aTimer;
        private ITimeStore _timeStore;
        private PersianCalendar _persianCalender;
        public string CloclString => DateTime.Now.ToString();
        #endregion

        #region Constructor
        public StatusBarViewModel(ITimeStore timeStore)
        {
            _persianCalender = new PersianCalendar();
            _timeStore = timeStore;
            _timeStore.CurrentTime = $"{_persianCalender.GetDayOfMonth(DateTime.Now)}/{_persianCalender.GetMonth(DateTime.Now)}/{_persianCalender.GetYear(DateTime.Now)} - {DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}";
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;
        }
        #endregion

        #region Methods
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string dateTime = $"{_persianCalender.GetYear(DateTime.Now)}/{_persianCalender.GetDayOfMonth(DateTime.Now)}/{_persianCalender.GetMonth(DateTime.Now)} - {DateTime.Now.ToString("HH:mm:ss", CultureInfo.InvariantCulture)}";
            _timeStore.CurrentTime = dateTime;
            OnProperychanged(nameof(CloclString));
        }
        #endregion
    }
}
