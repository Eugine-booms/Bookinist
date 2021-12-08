using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Models;

using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;

using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Buyer> _bauerRepository;
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IRepository<Deal> _deal;


        #region  int BookCount Количество книг
        ///<summary> Количество книг
        private int _BookCount;
        ///<summary> Количество книг
        public int BooksCount
        {
            get => _BookCount;
            set => Set(ref _BookCount, value, nameof(BooksCount));
        }
        #endregion
        public ObservableCollection<BestSellersInfo> BestSellers { get; } = new ObservableCollection<BestSellersInfo>();


        #region Конструктор
        public StatisticViewModel(
            IRepository<Book> bookRepository, 
            IRepository<Buyer> bauerRepository, 
            IRepository<Seller> sellerRepository,
            IRepository<Deal> deal

            )
        {
            _bookRepository = bookRepository;
            _bauerRepository = bauerRepository;
            _sellerRepository = sellerRepository;
            _deal = deal;
        }

        public StatisticViewModel()
        {
        }
        #endregion


        #region Команда ComputerStatictic
        private ICommand _ComputerStaticticCommand;
        /// <summary>"Описание"</summary>
        public System.Windows.Input.ICommand ComputerStaticticCommand =>
        _ComputerStaticticCommand ??=
        new LambdaCommandAsync(OnComputerStaticticCommandExecuted);
        private async Task OnComputerStaticticCommandExecuted()
        {
            await ComputerDealsStatisticAsync();
            
        }

        private async Task ComputerDealsStatisticAsync()
        {
            var q = _deal.Items
                .GroupBy(d => d.Book.Id)
                .Select(deals => new { bookId = deals.Key, count = deals.Count() })
                .OrderByDescending(deals => deals.count)
                .Take(15)
                .Join(_bookRepository.Items,
                deals => deals.bookId,
                book => book.Id,
                (deals, book) => new BestSellersInfo() { Book = book, SellCount = deals.count });

            BestSellers.Clear();
            foreach (var item in await q.ToArrayAsync())
            {
                BestSellers.Add(item);
            }
           

        }
        #endregion
    }
}
