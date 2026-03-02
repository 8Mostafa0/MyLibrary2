using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System;

namespace MyLibrary.ViewModel.Commands
{
    public class OpenModalCommand : CommandBase
    {
        #region Dependencies
        private Func<IViewModelBase> _createViewModel;
        private ModalNavigationStore _modalNavigationStore;
        #endregion

        #region Contructor
        /// <summary>
        /// set vurrent view of modal navigation to Func of the inputed view model
        /// </summary>
        /// <param name="modalNavigationStore"></param>
        /// <param name="createViewModel"></param>
        public OpenModalCommand(ModalNavigationStore modalNavigationStore, Func<IViewModelBase> createViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            _createViewModel = createViewModel;
        }
        #endregion

        #region Execution
        /// <summary>
        /// </summary>
        /// <param name="parameter">no marametes needed</param>
        public override void Execute(object parameter)
        {
            _modalNavigationStore.CurrentViewModel = _createViewModel();
        }
        #endregion
    }
}
