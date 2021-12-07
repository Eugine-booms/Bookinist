using Bookinist.DAL.Entities;
using Bookinist.Interfaces;
using Bookinist.Services.Interfaces;

using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookinist.Services
{
    class SaleService : ISaleService
    {
        private readonly IRepository<Book> _books;
        private readonly IRepository<Deal> _deals;

        public SaleService(
            IRepository<Book> books, 
            IRepository<Deal> deals)
        {
            _books = books;
            _deals = deals;
        }

        public IEnumerable<Deal> Deals => _deals.Items;

        public async Task< Deal>  MakeDeal(string bookName, Seller seller, Buyer buyer, decimal price)
        {
            var book = await _books.Items.FirstOrDefaultAsync(b => b.Name == bookName).ConfigureAwait(false);
            if (book is null) return null;
            var deal = new Deal()
            {
               Book=book,
               Buyer=buyer,
               Seller=seller,
               Price=price
            };
            return await _deals.AddAsync(deal);
        }
    }
}
