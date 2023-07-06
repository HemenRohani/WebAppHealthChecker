using Microsoft.EntityFrameworkCore;
using WebAppHealthChecker.Domain.Entities;

namespace WebAppHealthChecker.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Save all entities in to database.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<SaveChangesResult> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public DbSet<User> Users { get; }
        public DbSet<WebApp> WebApps { get; }
    }
}
