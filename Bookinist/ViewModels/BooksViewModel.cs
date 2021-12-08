using Bookinist.DAL.Entities;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;
using Bookinist.View;

using MathCore.CSV;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel

    {
        private IRepository<Book> _bookRepository;
        private readonly IUserDialog userDialog;
        private readonly CollectionViewSource bookViewSource=new CollectionViewSource();
        public ICollectionView BookView=> bookViewSource?.View; 

        #region  ObservableCollection Books Коллекция книг
        ///<summary> Коллекция книг
        private ObservableCollection<Book> _Books;
        ///<summary> Коллекция книг
        public ObservableCollection<Book> Books
        {
            get => _Books;
            set
            {
                if (Set(ref _Books, value, nameof(Books)))
                {
                    bookViewSource.Source = Books;
                    OnPropertyChanged(nameof(BookView));  
                }
            }
        }
        #endregion

        #region Конструктор
        //public BooksViewModel() : this(new DebugBookRepository())
        //{
        //    if (!App.IsDesignTime)
        //        throw new InvalidOperationException("Использование конструктора предназначенного для дизайнера VS");
        //    _ = OnLoadDataCommandExecuted();
        //}
        public BooksViewModel(IRepository<Book> bookRepository, IUserDialog userDialog)
        {
            _bookRepository = bookRepository;
            this.userDialog = userDialog;
            bookViewSource.Filter += BookViewSource_Filter;
        }
        #endregion

        #region ФильтерКниг
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
        private void BookViewSource_Filter(object sender, FilterEventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(BookFilter)) return;
            e.Accepted = (e.Item as Book)?.Name?.ToLower().Contains(BookFilter?.ToLower() ?? "") ?? true;
        }
        #endregion


        #region  Book SelectedBook Выбранная книга
        ///<summary> Выбранная книга
        private Book _SelectedBook;
        ///<summary> Выбранная книга
        public Book SelectedBook
        {
            get => _SelectedBook;
            set => Set(ref _SelectedBook, value, nameof(SelectedBook));
        }
        #endregion


        #region Commands

        #region Команда AddBook
        private ICommand _AddBookCommand;
        /// <summary>"Описание"</summary>
        public ICommand AddBookCommand =>
        _AddBookCommand ??=
        new LambdaCommand(OnAddBookCommandExecuted, CanAddBookCommandExecute);
        private void OnAddBookCommandExecuted(object p)
        {
            
            if(!(p is Book new_book))
                new_book = new Book();
            if (!userDialog.Edit(new_book)) return;
            _Books.Add(_bookRepository.Add(new_book));
            _bookRepository.Add(new_book);

        }
        private bool CanAddBookCommandExecute(object p) => true;

        #endregion


        #region Команда RemoveBook
        private ICommand _RemoveBookCommand;
        /// <summary>"Описание"</summary>
        public ICommand RemoveBookCommand =>
        _RemoveBookCommand ??=
        new LambdaCommand(OnRemoveBookCommandExecuted, CanRemoveBookCommandExecute);
        private void OnRemoveBookCommandExecuted(object p)
        {
            var book = SelectedBook;
             _bookRepository.Remove(book.Id);

        }
        private bool CanRemoveBookCommandExecute(object p) => SelectedBook is Book book;
        
        #endregion


        #region Команда LoadData
        private ICommand _LoadDataCommand;
        /// <summary>"Описание"</summary>
        public ICommand LoadDataCommand =>
        _LoadDataCommand ??=
        new LambdaCommandAsync(OnLoadDataCommandExecuted, CanLoadDataCommandExecute);
        private async Task OnLoadDataCommandExecuted()
        {
            Books = new ObservableCollection<Book>(await _bookRepository.Items.ToArrayAsync());
            bookViewSource.SortDescriptions.Add(new SortDescription(nameof(Book.Name), ListSortDirection.Ascending));
        }
        private bool CanLoadDataCommandExecute() => true;

        #endregion

        #endregion
    }
}
