using MyLibrary.Model.Models;
using MyLibrary.Model.Repositories;
using MyLibrary.ViewModel.Stores;
using System;
using System.Collections.Generic;

namespace MyLibrary.ViewModel.Commands.BooksCommands
{
    public class AddNewBookCommand : CommandBase, IAddNewBookCommand
    {
        #region Dependencies
        private IBooksStore _booksStore;
        private IBooksRepository _booksRepository;
        private IMessageBoxStore _messageBoxStore;
        #endregion


        #region Contructor
        /// <summary>
        /// Create New Book And Store It In Database
        /// </summary>
        public AddNewBookCommand(
            IBooksStore booksStore,
            IBooksRepository booksRepository,
            IMessageBoxStore messageBoxStore
            )
        {
            _booksStore = booksStore;
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
            if (_booksStore.SelectedBook.Name is null || _booksStore.SelectedBook.Name == "")
            {
                _messageBoxStore.Show("لطفا نام کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksStore.SelectedBook.Publisher is null || _booksStore.SelectedBook.Publisher == "")
            {
                _messageBoxStore.Show("لطفا منتشرکننده کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksStore.SelectedBook.Subject is null || _booksStore.SelectedBook.Subject == "")
            {
                _messageBoxStore.Show("لطفا نوع کتاب را وارد کنید", "افزودن کتاب");
                return;
            }
            if (_booksStore.SelectedBook.PublicationDate is null || _booksStore.SelectedBook.PublicationDate == "")
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را وارد کنید", "افزودن کتاب");
                return;
            }

            if (!int.TryParse(_booksStore.SelectedBook.PublicationDate, out _) && _booksStore.SelectedBook.PublicationDate.Length != 4)
            {
                _messageBoxStore.Show("لطفا تاریخ انتشار کتاب را عدد و به طول 4 کاراکتر وارد کنید", "افزودن کتاب");
                return;
            }

            List<Book> CopiesOfBook = await _booksRepository.GetBooksByName(_booksStore.SelectedBook.Name);
            bool IsBookExists = false;
            foreach (Book book in CopiesOfBook)
            {
                if (book.Name == _booksStore.SelectedBook.Name && book.Publisher == _booksStore.SelectedBook.Publisher)
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
                Name = _booksStore.SelectedBook.Name,
                Publisher = _booksStore.SelectedBook.Publisher,
                Subject = _booksStore.SelectedBook.Subject,
                PublicationDate = _booksStore.SelectedBook.PublicationDate,
                Tier = _booksStore.SelectedBook.Tier,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _booksStore.AddNewBook(NewBook);
        }
        #endregion
    }
}
