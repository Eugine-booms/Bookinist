using Bookinist.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.Models
{
  internal  class BestSellersInfo
    {
        public Book Book { get; set; }
        public int SellCount {get; set; }
        public decimal SumCount { get; set; }
    }
}
