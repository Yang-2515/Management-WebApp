
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;
using WebApp.Domain.Interfaces;
using WebApp.Infragtructure;

namespace WebApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext _dbContext;

        public UnitOfWork(AppContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*public IAsyncRepository<T> AsyncRepository<T>() where T : class
        {
            return new RepositoryBase<T>(_dbContext);
        }*/

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}