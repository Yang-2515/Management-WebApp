using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task BeginTransaction();

        /// <summary>
        /// Commits the current transaction (does nothing when none exists).
        /// </summary>
        Task CommitTransaction();

        /// <summary>
        /// Rolls back the current transaction (does nothing when none exists).
        /// </summary>
        Task RollbackTransaction();
    }
}