using Bookinist.DAL.Entities.Base;

namespace Bookinist.DAL.Entities
{
    public class Seller : Person
    {
        public override string ToString() => $"Продавец {Name} {Surname} {Patronymic}";
    }
}
