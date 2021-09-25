using Application.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Data.Mappings.Users
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Usuario", "App");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id);

            builder.Property(u => u.Username).HasMaxLength(150);
            builder.Property(u => u.Email);
            builder.Property(u => u.Password).HasMaxLength(128);
        }
    }
}
