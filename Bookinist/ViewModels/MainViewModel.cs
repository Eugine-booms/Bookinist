using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;


using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
  public  class MainViewModel: ViewModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly ISaleService _saleService;
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IRepository<Buyer> _bauerRepository;
        private readonly IRepository<Deal> _deal;



        #region  ViewModel CurentModel Текущее дочерняя модель представление 
        ///<summary> Текущее дочерняя модель представление 
        private ViewModel _CurentModel;
        ///<summary> Текущее дочерняя модель представление 
        public ViewModel CurentModel
        {
            get => _CurentModel;
            set => Set(ref _CurentModel, value, nameof(CurentModel));
        }
        #endregion




        #region Конструктор
        public MainViewModel(
            IRepository<Book> bookRepository,
            ISaleService saleService,
            IRepository<Seller> sellerRepository,
            IRepository<Buyer> bauerRepository,
            IRepository<Deal> deal


            )
        {
            _bookRepository = bookRepository;
            _saleService = saleService;
            _sellerRepository = sellerRepository;
            _bauerRepository = bauerRepository;
            this._deal = deal;

            // Test();
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


        #region Команды 
        #region Команда ShowBooksView
        private ICommand _ShowBooksViewCommand;
        /// <summary>"Описание"</summary>
        public ICommand ShowBooksViewCommand =>
        _ShowBooksViewCommand ??=
        new LambdaCommand(OnShowBooksViewCommandExecuted, CanShowBooksViewCommandExecute);
        private void OnShowBooksViewCommandExecuted(object p)
        {
            CurentModel = new BooksViewModel(_bookRepository);
        }
        private bool CanShowBooksViewCommandExecute(object p) => true;

        #endregion

        #region Команда ShowBuyrsView
        private ICommand _ShowBuyrsViewCommand;
        /// <summary>"Описание"</summary>
        public ICommand ShowBuyrsViewCommand =>
        _ShowBuyrsViewCommand ??=
        new LambdaCommand(OnShowBuyrsViewCommandExecuted, CanShowBuyrsViewCommandExecute);
        private void OnShowBuyrsViewCommandExecuted(object p)
        {
            CurentModel = new  BuyersViewModel(_bauerRepository);
        }
        private bool CanShowBuyrsViewCommandExecute(object p) => true;
        
        #endregion


        #region Команда ShowStatisticView
        private ICommand _ShowStatisticViewCommand;
        /// <summary>"Описание"</summary>
        public ICommand ShowStatisticViewCommand =>
        _ShowStatisticViewCommand ??=
        new LambdaCommand(OnShowStatisticViewCommandExecuted, CanShowStatisticViewCommandExecute);
        private void OnShowStatisticViewCommandExecuted(object p)
        {
            CurentModel = new StatisticViewModel(
                _bookRepository,
                _bauerRepository,
                _sellerRepository,
                _deal
                );
        }
        private bool CanShowStatisticViewCommandExecute(object p) => true;
        #endregion 
        #endregion


    }
}
