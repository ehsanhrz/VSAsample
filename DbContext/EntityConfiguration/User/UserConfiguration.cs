using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContext.EntityConfiguration.User;

public class UserConfiguration : IEntityTypeConfiguration<Entities.Users.User>
{
    public void Configure(EntityTypeBuilder<Entities.Users.User> builder)
    {
        builder.ToTable("Users");

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(150);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(150);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.HasMany(u => u.UserAddresses)
            .WithOne(ua => ua.User)
            .HasForeignKey(ua => ua.UserId)
            .HasPrincipalKey(u => u.Id);
        
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}