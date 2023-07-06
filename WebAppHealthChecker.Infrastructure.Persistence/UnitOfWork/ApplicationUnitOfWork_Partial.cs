using WebAppHealthChecker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebAppHealthChecker.Infrastructure.Persistence.Context;

public partial class ApplicationUnitOfWork
{
    public DbSet<User> Users => _context.Set<User>();
    public DbSet<WebApp> WebApps => _context.Set<WebApp>();
}