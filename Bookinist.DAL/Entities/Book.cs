using Bookinist.DAL.Entities.Base;

using System;
using System.Text;

namespace Bookinist.DAL.Entities
{
    public class Book : NamedEntity
    {
        public virtual Category Category { get; set; }
    }
}
