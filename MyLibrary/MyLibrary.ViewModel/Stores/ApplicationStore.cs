using MyLibrary.Model.Base;
using MyLibrary.Model.Models;
using System;

namespace MyLibrary.ViewModel.Stores
{
    public class ApplicationStore : PropertyChangedBase, IApplicationStore
    {
        #region EVENTS

        public Action SelectedClientChanged;
        public Action SelectedBookChanged;
        public Action SelectedloanChanged;
        public Action SelectedReservBookChanged;
        public Action IsDatabaseConnectedChanged;
        public Action IsLoadingChanged;

        #endregion

        #region FIELDS

        private Client _selectedClient;
        private Book _selectedBook;
        private Loan _selectedLoan;
        private ReservedBook _selectedReservBook;

        private bool _isDatabaseConnected;
        private bool _isLoading;

        #endregion

        #region PROPERTIES
        public Client SelectedClient
        {
            get => _selectedClient;
            set => SetProperty(ref _selectedClient, value, SelectedClientChanged);
        }
        public Book SelectedBook
        {
            get => _selectedBook;
            set => SetProperty(ref _selectedBook, value, SelectedBookChanged);
        }
        public Loan SelectedLoan
        {
            get => _selectedLoan;
            set => SetProperty(ref _selectedLoan, value, SelectedloanChanged);
        }
        public ReservedBook SelectedReservedBook
        {
            get => _selectedReservBook;
            set => SetProperty(ref _selectedReservBook, value, SelectedReservBookChanged);
        }
        public bool IsDatabaseConnected
        {
            get => _isDatabaseConnected;
            set => SetProperty(ref _isDatabaseConnected, value, IsDatabaseConnectedChanged);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value, IsLoadingChanged);
        }



        #endregion
    }
}
