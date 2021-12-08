using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

using System;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private IRepository<Book> _bookRepository;

        public BooksViewModel()
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Использование конструктора предназначенного для дизайнера VS");





        }

        public BooksViewModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
    }
}
