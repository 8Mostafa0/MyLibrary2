using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using MyLibrary.ViewModel.ViewModels;
using System;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class AddNewBookCommand : CommandBase
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IBooksViewModel _booksViewModel;
        private BooksRepository _booksRepository;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// Create New Book And Store It In Database
        /// </summary>
        /// <param name="booksStore"></param>
        /// <param name="booksViewModel"></param>
        /// <param name="booksRepository"></param>
        public AddNewBookCommand(IBooksStore booksStore, IBooksViewModel booksViewModel, BooksRepository booksRepository, IMessageBoxStore messageBoxStore)
        {
            _booksStore = booksStore;
            _booksViewModel = booksViewModel;
            _booksRepository = booksRepository;
            _messageBoxStore = messageBoxStore;
        }

        #endregion


        #region Execution
        /// <summary>
        ///  Checks To Validate Book Data First then Add New Book To Database Using BookStore Class.
        /// </summary>
        /// <param name="parameter">No Perameer Needed This Method Gets Its Data From BooksViewModel 
        /// Data : Name,Publisher,Subject,PublicationDate
        /// </param>
        public override async void Execute(object parameter)
        {
            if (_booksViewModel.Name is null || _booksViewModel.Name == "")
            {
                _messageBoxStore.Show("لطفا نام کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksViewModel.Publisher is null || _booksViewModel.Publisher == "")
            {
                _messageBoxStore.Show("لطفا منتشرکننده کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksViewModel.Subject is null || _booksViewModel.Subject == "")
            {
                _messageBoxStore.Show("لطفا نوع کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksViewModel.PublicationDate is null || _booksViewModel.PublicationDate == "")
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را وارد کنید", "افزودن کتاب");
                return;
            }

            if (!int.TryParse(_booksViewModel.PublicationDate, out _) && _booksViewModel.PublicationDate.Length != 4)
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را عدد و به طول 4 کاراکتر وارد کنید", "افزودن کتاب");
                _booksViewModel.PublicationDate = "";
                return;
            }

            List<Book> CopiesOfBook = await _booksRepository.GetBooksByName(_booksViewModel.Name);
            bool IsBookExists = false;
            foreach (Book book in CopiesOfBook)
            {
                if (book.Name == _booksViewModel.Name && book.Publisher == _booksViewModel.Publisher)
                {
                    IsBookExists = true;
                }
            }
            if (IsBookExists)
            {
                _messageBoxStore.Show("این کتاب با این منشتر کننده موجود است", "افزودن کتاب");
                return;
            }
            Book NewBook = new Book()
            {
                Name = _booksViewModel.Name,
                Publisher = _booksViewModel.Publisher,
                Subject = _booksViewModel.Subject,
                PublicationDate = _booksViewModel.PublicationDate,
                Tier = _booksViewModel.Tier,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _booksStore.AddNewBook(NewBook);
        }
        #endregion
    }
}
