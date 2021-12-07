using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;

using MathCore.ViewModels;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bookinist.ViewModels
{
  public  class MainViewModel: ViewModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly ISaleService _saleService;
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IRepository<Buyer> _bauerRepository;






        #region Конструктор
        public MainViewModel(
            IRepository<Book> bookRepository,
            ISaleService saleService,
            IRepository<Seller> sellerRepository,
            IRepository<Buyer> bauerRepository

            )
        {
            _bookRepository = bookRepository;
            _saleService = saleService;
            _sellerRepository = sellerRepository;
            _bauerRepository = bauerRepository;

            Test();
        }

        private async void Test()
        {
            var books = _bookRepository.Items.Take(10).ToArray();
            var deal_count = _saleService.Deals.Count();
            var book = _bookRepository.Get(5).Name;
            var deal = await _saleService.MakeDeal(book, _sellerRepository.Get(3), _bauerRepository.Get(2), 1450).ConfigureAwait(false);
            var stop = Stopwatch.StartNew();
            var deal_count1 = _saleService.Deals.Count();
        }
        #endregion


        #region  string Title Заголовок
        ///<summary> Заголовок
        private string _Title ="Главное окно";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value, nameof(Title));
        }
        #endregion

    }
}
