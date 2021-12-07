using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> Items { get; }
        T Get(int id);
        Task<T> GetAsync(int id, CancellationToken Cancel=default);
        T Add(T items);
        Task <T> AddAsync(T items, CancellationToken Cancel = default);
        T Update(T items);
        Task<T> UpdateAsync(T items, CancellationToken Cancel = default);
        T Remove(int id);
        Task<T> RemoveAsync(int id, CancellationToken Cancel = default);
    }
}
