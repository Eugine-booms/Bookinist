using Bookinist.DAL.Entities;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel

    {
        private IRepository<Book> _bookRepository;
        private readonly CollectionViewSource bookViewSource;
        public ICollectionView BookView => bookViewSource.View;
        public IEnumerable<Book> Books => _bookRepository.Items;
        #region Конструктор
        public BooksViewModel() : this(new DebugBookRepository())
        {
            if (!App.IsDesignTime)
                throw new InvalidOperationException("Использование конструктора предназначенного для дизайнера VS");
        }
        public BooksViewModel(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
            bookViewSource = new CollectionViewSource();
            bookViewSource.Source = _bookRepository.Items;
            bookViewSource.Filter += BookViewSource_Filter;
            bookViewSource.SortDescriptions.Add(new SortDescription(nameof(Book.Name), ListSortDirection.Ascending));
        }

        private void BookViewSource_Filter(object sender, FilterEventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(BookFilter)) return;
            e.Accepted = (e.Item as Book)?.Name?.ToLower().Contains(BookFilter?.ToLower() ?? "") ?? true;
        }
        #endregion
        #region  string BookFilter Строка фильтра для книг
        ///<summary> Строка фильтра для книг
        private string _BookFilter;
        ///<summary> Строка фильтра для книг
        public string BookFilter
        {
            get => _BookFilter;
            set
            {
                if (Set(ref _BookFilter, value, nameof(BookFilter)))
                    BookView.Refresh();
            }
        }
        #endregion
    }
}
