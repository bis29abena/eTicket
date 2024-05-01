using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTicket.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRespository<T> where T : class, IEntityBase, new()
    {
        private readonly ApplicationDbContext _db;

        public EntityBaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(T entity) 
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id, T entity)
        {
            entity = await _db.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _db.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id) => await _db.Set<T>().FirstOrDefaultAsync(n => n.Id == id);
            

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry = _db.Entry<T>(entity);
            entityEntry.State =  EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
