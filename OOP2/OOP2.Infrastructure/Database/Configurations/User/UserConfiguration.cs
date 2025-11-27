using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OOP2.Infrastructure.Database.Configurations.User
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.User.User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.FirstName)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(Domain.Entities.User.User.MaxNameLength);

            builder.Property(u => u.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(Domain.Entities.User.User.MaxNameLength);

            builder.Property(u => u.UserName)
                .HasColumnName("username")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(u => u.AdressCity)
                .HasColumnName("address_city");

            builder.Property(u => u.AdressStreet)
                .HasColumnName("address_street");

            builder.Property(u => u.CoordinateLat)
                .HasColumnName("geo_lat")
                .IsRequired();

            builder.Property(u => u.CoordinateLng)
                .HasColumnName("geo_lng")
                .IsRequired();

            builder.Property(u => u.Website)
                .HasColumnName("website")
                .HasMaxLength(Domain.Entities.User.User.URLMaxLength);

            builder.Property(u => u.BirthDate)
                .HasColumnName("birth_date");

            builder.Property(u => u.IsActive)
                .HasColumnName("is_active")
                .IsRequired();

            builder.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .IsRequired();
        }
    }
}
