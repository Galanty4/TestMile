using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interfaces.Repositories;
using DAL.TestMilDbContext;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly TestMilDBcontext DbContext;

        protected BaseRepository(TestMilDBcontext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<TEntity> GetAsync(int id) => await DbContext.Set<TEntity>().FindAsync(id);
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await DbContext.Set<TEntity>().ToListAsync();

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbContext.Set<TEntity>().AddAsync(entity);
            await SaveAsync();

            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var baseEntities = entities.ToList();

            await DbContext.Set<TEntity>().AddRangeAsync(baseEntities);
            await SaveAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await DbContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }

            entity.Delete();
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
