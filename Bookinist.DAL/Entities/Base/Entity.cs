using Bookinist.Interfaces;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bookinist.DAL.Entities.Base
{
    public abstract class Entity  : IEntity
    {
        public int Id { get; set; }
    }
}
