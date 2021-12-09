using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookinist.ViewModels
{
    class BookEditorWindowViewModel : ViewModel
    {
        private Book _book;

        public BookEditorWindowViewModel(Book book=null)
        {
            _book = book?? new Book() {Category=new Category() { } };

            CategoryList = App.Services.GetRequiredService<IRepository<Category>>().Items.ToList();
            Name = book.Name;
            Category = _book.Category;   
        }

        public BookEditorWindowViewModel()
        {
            if (App.IsDesignTime) throw new InvalidOperationException("Конструктор не для рантайма");
        }


        public int BookId => _book.Id;


        #region  List<Categiry> CategoryList ""
        ///<summary> ""
        private List<Category> _CategoryList;
        ///<summary> ""
        public List<Category> CategoryList
        {
            get => _CategoryList;
            set => Set(ref _CategoryList, value, nameof(CategoryList));
        }
        #endregion


        #region  string Name Название книги
        ///<summary> Название книги
        private string _Name;
        ///<summary> Название книги
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value, nameof(Name));
        }
        #endregion


        #region   Category
        ///<summary> ""
        private Category _Category;
        ///<summary> ""
        public Category Category
        {
            get => _Category;
            set => Set(ref _Category, value, nameof(Category));
        }
        #endregion

    }
}
