using Api.Data.Context;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
                entity.CreatedAt = DateTime.UtcNow;

                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public async Task<T> Update(T entity)
        {
            try
            {
                var result = await _dbSet.SingleOrDefaultAsync(e => e.Id == entity.Id);
                if (result == null)
                    return null;

                entity.UpdatedAt = DateTime.UtcNow;
                _context.Entry(result).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        public async Task<T> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
                if (result == null)
                    return null;
                _dbSet.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> Select(Guid id)
        {
            try
            {
                return await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<T> Select()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Exists(Guid id)
        {
            try
            {
                return await _dbSet.AnyAsync(e => e.Id == id);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}







