using Microsoft.EntityFrameworkCore;
using OOP2.Domain.Entities.Company;
using OOP2.Domain.Entities.User;

namespace OOP2.Infrastructure.Database
{
    public sealed class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.HasDefaultSchema("public");
        }
    }
}
