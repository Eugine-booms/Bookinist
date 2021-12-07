using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private IRepository<Book> _bookRepository;

        public BooksViewModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
    }
}
