using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.Infrastructure.DebugServices
{
    class DebugBookRepository : IRepository<Book>
    {
        private Book[] _book;


        public DebugBookRepository()
        {
            _book = Enumerable.Range(1, 100)
            .Select(i => new Book()
            {
                Id = i,
                Name = $"Книга {i}",
            }).ToArray();
            var catgories = Enumerable.Range(1, 10)
                .Select(cat => new Category()
                {
                    Id = cat,
                    Name = $"Категория {cat}",
                    
                }).
                ToArray();
            var rnd = new Random();
            foreach (var book in _book)
            {
                book.Category = rnd.NextItem(catgories);
                book.Category.Books.Add(book);
            }
            Items = _book.AsQueryable();
        }

        public IQueryable<Book> Items { get; }

        public Book Add(Book items)
        {
            throw new NotImplementedException();
        }

        public Task<Book> AddAsync(Book items, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetAsync(int id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Book Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Book> RemoveAsync(int id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Book Update(Book items)
        {
            throw new NotImplementedException();
        }

        public Task<Book> UpdateAsync(Book items, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
