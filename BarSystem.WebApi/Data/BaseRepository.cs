using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace BarSystem.WebApi.Data
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        private readonly BarSystemDbContext _dbContext;

        public BaseRepository(BarSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, int id)
        {
            var existingEntity = await GetAsync(id);

            if (existingEntity is null)
                return null;

            UpdateEntity(existingEntity, entity);

            await _dbContext.SaveChangesAsync();

            return existingEntity;
        }

        protected abstract void UpdateEntity(T existingEntity, T newEntity);

        public virtual async Task<T> GetAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entityToDelete = await GetAsync(id);
            _dbContext.Set<T>().Remove(entityToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var entityList = await _dbContext.Set<T>().ToListAsync();
            return entityList;
        }

        public virtual async Task<bool> EntityExistsAsync(int id)
        {
            var entityExists = await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
            return entityExists;
        }
    }
}
