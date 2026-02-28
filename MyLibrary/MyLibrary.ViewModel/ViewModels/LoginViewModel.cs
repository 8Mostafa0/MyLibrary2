using System.Windows.Input;

namespace MyLibrary.ViewModel.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Dependencies
        private string _password;
        public bool FirstOpen { get; }
        public string Title { get; set; }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnProperychanged(nameof(Password));
            }
        }
        #endregion

        #region Commands
        public ICommand CloseAppCommand { get; }
        public ICommand LoginCommand { get; }

        #endregion

        #region Constructor
        public LoginViewModel(ModalNavigationStore modalNavigationStore, LoanRepository loanRepository, DbContextFactory dbContextFactory, SettingsStore settingsStore)
        {
            CloseAppCommand = new CloseAppCommand();
            LoginCommand = new LoginCommand(this, modalNavigationStore, loanRepository, dbContextFactory, settingsStore);
            if (new SettingsStore().GetHashedPassword().IsNullOrEmpty())
            {
                FirstOpen = true;
                Title = "رمزی برای پنل مشخص کنید";
            }
            else
            {
                Title = "رمز عبور خود را وارد کنید";
            }
        }
        #endregion
    }
}
