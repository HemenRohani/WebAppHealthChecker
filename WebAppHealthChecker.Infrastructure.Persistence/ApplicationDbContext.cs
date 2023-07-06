using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAppHealthChecker.Domain.Common;
using WebAppHealthChecker.Domain.Entities;
using WebAppHealthChecker.Infrastructure.Persistence.Extensions;

namespace WebAppHealthChecker.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("base");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.RegisterAllEntities<BaseEntity>(typeof(User).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
