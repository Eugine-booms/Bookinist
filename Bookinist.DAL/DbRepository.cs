using Bookinist.DAL.Context;
using Bookinist.DAL.Entities.Base;
using Bookinist.Interfaces;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bookinist.DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly BookinistDb _db;
        private readonly DbSet<T> _set;
        public bool AutosaveChanges = true;
        public DbRepository(BookinistDb db)
        {
            _db = db;
            _set = _db.Set<T>();

        }



        public virtual IQueryable<T> Items => _set;

        public T Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutosaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken сancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            if (AutosaveChanges)
                await _db.SaveChangesAsync(сancel).ConfigureAwait(false);
            return item;
        }

        public T Get(int id) => Items.SingleOrDefault(item => item.Id == id);

        public async Task<T> GetAsync(int id, CancellationToken сancel = default) => await Items
            .SingleOrDefaultAsync(item => item.Id == id, сancel)
            .ConfigureAwait(false);


        public T Remove(int id)
        {
            ////если сущность большая это может быть долго
            //var item = Get(id);
            //if (item is null) throw new KeyNotFoundException("Id не найден в базе данных");
            //_db.Entry(item).State=EntityState.Deleted;
            var items = _db.Remove(new T { Id = id });
            if (AutosaveChanges)
                _db.SaveChanges();
            return items.Entity;
        }

        public async Task<T> RemoveAsync(int id, CancellationToken cancel = default)
        {
            var items = _db.Remove(new T { Id = id });
            if (AutosaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return items.Entity;
        }

        public T Update(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutosaveChanges)
                _db.SaveChanges();
            return item;
        }

        public async Task<T> UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            if (AutosaveChanges)
                await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
            return item;
        }
    }
}
