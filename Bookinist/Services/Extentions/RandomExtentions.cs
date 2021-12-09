using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.Services
{
  static  class RandomExtentions
    {
        public static T NextItem<T>(this Random rnd, params T[] items)
        {
            return items[rnd.Next(items.Length)];
        }
    }
}
