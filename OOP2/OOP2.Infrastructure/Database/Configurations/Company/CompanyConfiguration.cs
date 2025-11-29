using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OOP2.Infrastructure.Database.Configurations.Company
{
    internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Domain.Entities.Company.Company>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Company.Company> builder)
        {
            builder.ToTable("companies");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Name)
                .HasColumnName("name");  
            
            builder.Property(c => c.CreatedAt)
                .HasColumnName("created_at");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("updated_at");
        }
    }
}
