using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
    class StatisticViewModel : ViewModel
    {
        private IRepository<Book> _bookRepository;
        private IRepository<Buyer> _bauerRepository;
        private IRepository<Seller> _sellerRepository;
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
        new LambdaCommandAsync(OnComputerStaticticCommandExecuted, CanComputerStaticticCommandExecute);
        private async Task OnComputerStaticticCommandExecuted()
        {
            BooksCount = await _bookRepository.Items.CountAsync();
            var deals = await _deal.Items.ToArrayAsync();

            var bestsellers = deals
                .GroupBy(b => b.Book)
                .Select(book_deals => new { Book = book_deals.Key, count = book_deals.Count() })
                .OrderBy(x => x.count)
                .Take(5);
                
        }
        private bool CanComputerStaticticCommandExecute() => true;
        #endregion



    }
}
