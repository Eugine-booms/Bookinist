using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;

        public BuyersViewModel( IRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }
    }
}
