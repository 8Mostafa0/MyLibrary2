using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Servicies
{
    public class NavigationService : INavigationService
    {
        #region Dependencies
        private readonly Func<IViewModelBase> _createMainViewModel;

        public NavigationStore NavigationStore;
        #endregion

        #region Contructor
        /// <summary>
        /// set value of Func view to main view of main navigation view
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="createMainViewModel"></param>
        public NavigationService(NavigationStore navigationStore, Func<IViewModelBase> createMainViewModel)
        {
            NavigationStore = navigationStore;
            _createMainViewModel = createMainViewModel;
        }
        #endregion

        #region Methods
        public void NavigateMainVewModel()
        {
            NavigationStore.MainContentViewModel = _createMainViewModel();
        }

        public void NavigateContentViewModel()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
