using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Bookinist.DAL
{
    class BooksRepository : DbRepository<Book>
    {
        public BooksRepository(BookinistDb db) : base(db) { }
        public override IQueryable<Book> Items => base.Items.Include(item => item.Category);
    }
}
