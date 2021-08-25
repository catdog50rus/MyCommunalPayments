using Microsoft.EntityFrameworkCore;
using MyCommunalPayments.Data.Context;
using MyCommunalPayments.Data.DBModels.Models.Base;
using MyCommunalPayments.Data.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCommunalPayments.Data.Repositories.Impl.SQL.Base
{
    public abstract class BaseSqlRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseDbModel, new()
    {
        protected readonly DBContext  _context;

        private readonly DbSet<TEntity> _dbset;

        protected virtual IQueryable<TEntity> Items => _dbset;

        public BaseSqlRepository(DBContext context)
        {
             _context = context;
            _dbset = _context.Set<TEntity>();
        }

        public virtual async Task<ICollection<TEntity>> GetEntitiesAsync(CancellationToken cancel = default)
        {
            var result = await Items
                .AsNoTracking()
                .ToArrayAsync(cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<TEntity> GetEntityAsync(int id, CancellationToken cancel = default)
        {
            if (id <= 0)
                throw new ArgumentException("Должен быть больше 0", nameof(id));

            var entity = await Items
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
            
            return entity;
        }

        public async Task<TEntity> CreateEntityAsync(TEntity entity, bool isSaveChanged = true, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Added;
            if (isSaveChanged)
               await _context
                    .SaveChangesAsync(cancel)
                    .ConfigureAwait(false);

            return entity;
        }

        public async Task UpdateEntityAsync(TEntity entity, bool isSaveChanged = true, CancellationToken cancel = default)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Entry(entity).State = EntityState.Modified;
            if (isSaveChanged)
                await _context
                     .SaveChangesAsync(cancel)
                     .ConfigureAwait(false);
        }

        public async Task DeleteEntityAsync(int id, bool isSaveChanged = true, CancellationToken cancel = default)
        {
            if(id <= 0)
                throw new ArgumentException("Должен быть больше 0", nameof(id));

            _context.Remove(new TEntity { Id = id });

            if (isSaveChanged)
                await _context
                    .SaveChangesAsync(cancel)
                    .ConfigureAwait(false);
        }

        public async Task SaveChangedAsync(CancellationToken cancel = default)
        {
            await _context
                    .SaveChangesAsync(cancel)
                    .ConfigureAwait(false);
        }
    }
}
