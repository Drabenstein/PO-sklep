using Microsoft.EntityFrameworkCore;
using PO_sklep.Models;
using PO_sklep.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PO_sklep.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected POsklepContext DbContext { get; }

        public GenericRepository(POsklepContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<T> AddAsync(T t)
        {
            DbContext.Set<T>().Add(t);
            await DbContext.SaveChangesAsync();
            return t;

        }

        public async Task<int> CountAsync() => await DbContext.Set<T>().CountAsync();

        public virtual async Task<int> DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match) => await DbContext.Set<T>().Where(match).ToListAsync();

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match) => await DbContext.Set<T>().SingleOrDefaultAsync(match);

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate) => await DbContext.Set<T>().Where(predicate).ToListAsync();

        public virtual async Task<ICollection<T>> GetAllAsync() => await DbContext.Set<T>().ToListAsync();

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = DbContext.Set<T>();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbContext.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id) => await DbContext.Set<T>().FindAsync(id);
        public async virtual Task<int> SaveAsync() => await DbContext.SaveChangesAsync();
        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
            {
                return null;
            }
            T existing = await DbContext.Set<T>().FindAsync(key);
            if (existing != null)
            {
                DbContext.Entry(existing).CurrentValues.SetValues(t);
                await DbContext.SaveChangesAsync();
            }
            return existing;
        }


        #region Dispose pattern implementation
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
                this.disposed = true;
            }
        }
        #endregion
    }
}