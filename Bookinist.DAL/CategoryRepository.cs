using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Bookinist.DAL
{
    class CategoryRepository : DbRepository<Category>
    {
        public CategoryRepository(BookinistDb db) : base(db)
        {
        }

        public override IQueryable<Category> Items => base.Items.Include(item => item.Books);
    }
