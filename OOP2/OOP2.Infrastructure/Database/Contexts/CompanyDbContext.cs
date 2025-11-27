using Microsoft.EntityFrameworkCore;
using OOP2.Domain.Entities.Company;

namespace OOP2.Infrastructure.Database
{
    public sealed class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyDbContext).Assembly);
            modelBuilder.HasDefaultSchema("public");
        }
    }
}
