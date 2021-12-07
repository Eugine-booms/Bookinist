using Bookinist.DAL.Entities.Base;

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookinist.DAL.Entities
{
    public class Deal : Entity
    {
        public virtual Book  Book{get; set;}
        public virtual Seller Seller { get; set; }
        public  virtual Buyer Buyer { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public virtual decimal Price { get; set; }

    }
}
