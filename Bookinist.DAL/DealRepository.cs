using Bookinist.DAL.Context;
using Bookinist.DAL.Entities;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Bookinist.DAL
{
    class DealRepository : DbRepository<Deal>
    {
        public DealRepository(BookinistDb db) : base(db)
        {
        }

        public override IQueryable<Deal> Items => 
            base.Items
            .Include(item => item.Book)
            .Include(item => item.Seller)
            .Include(item => item.Buyer);
}
