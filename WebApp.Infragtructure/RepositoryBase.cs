using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Domain.Interfaces;
using WebApp.Infragtructure;
using AppContext = WebApp.Infragtructure.AppContext;

namespace WebApp.Infrastructure
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly AppContext appContext;

        public RepositoryBase(AppContext dbContext)
        {
            appContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await appContext.SaveEntitiesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await appContext.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefaultAsync(expression);
        }
        public async Task<T> FindAsync(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await appContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }
    }
}