using MyLibrary.ViewModel.Commands.LoginCommands;
using MyLibrary.ViewModel.Factory;

namespace MyLibrary.ViewModel.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
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
        public ICloseAppCommand CloseAppCommand { get; }
        public ILoginCommand LoginCommand { get; }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            CloseAppCommand = ClassFactory.CreateCloseAppCommand();
            LoginCommand = ClassFactory.CreateLoginCommand();
            if (ClassFactory.CreateSettingsStore().GetHashedPassword() == null)
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
