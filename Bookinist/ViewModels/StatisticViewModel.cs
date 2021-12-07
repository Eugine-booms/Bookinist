using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private IRepository<Book> _bookRepository;
        private IRepository<Buyer> _bauerRepository;
        private IRepository<Seller> _sellerRepository;

        public StatisticViewModel(IRepository<Book> bookRepository, IRepository<Buyer> bauerRepository, IRepository<Seller> sellerRepository)
        {
            _bookRepository = bookRepository;
            _bauerRepository = bauerRepository;
            _sellerRepository = sellerRepository;
        }
    }
}
