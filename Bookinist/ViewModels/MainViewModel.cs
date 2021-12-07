using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookinist.ViewModels
{
  public  class MainViewModel: ViewModel
    {
        private readonly IRepository<Book> _bookRepository;


      
        
        

        #region Конструктор
        public MainViewModel(IRepository<Book> BookRepository)
        {
            _bookRepository = BookRepository;
            var books = BookRepository.Items.Take(10).ToArray();
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
