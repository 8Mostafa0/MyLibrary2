using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Servicies
{
    public class ModalNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        #region Dependencies
        private readonly ModalNavigationStore _navigationStore;
        private readonly Func<TViewModel> _createView;
        #endregion
        #region Contructor
        /// <summary>
        /// set tha value of Func view to the current view of modal navigation
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="createView"></param>
        public ModalNavigationService(ModalNavigationStore navigationStore, Func<TViewModel> createView)
        {
            _navigationStore = navigationStore;
            _createView = createView;
        }
        #endregion

        #region Methods
        public void NavigateContentViewModel()
        {
            _navigationStore.CurrentViewModel = _createView();
        }
        #endregion
    }
}
