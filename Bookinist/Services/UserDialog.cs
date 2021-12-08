using Bookinist.DAL.Entities;
using Bookinist.Services.Interfaces;
using Bookinist.View;
using Bookinist.View.Windows;
using Bookinist.ViewModels;

using MathCore.WPF.Services;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.Services
{
  internal   class UserDialog : IUserDialog
    {
        public bool Edit(Book book)
        {
            var book_editor_vm = new BookEditorWindowViewModel(book);
            var addBookWindow = new BookEditorWindow();
            addBookWindow.DataContext = book_editor_vm;

            if (addBookWindow.ShowDialog()!=true) return false;
            book.Name = book_editor_vm.Name;
            book.Category = book_editor_vm.Category;

            return false;
        }
    }
}
