using Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbContext.EntityConfiguration.User;

public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable("UserAddresses");

        builder.Property(e => e.Address).HasMaxLength(255);
        builder.Property(e => e.PostalCode).HasMaxLength(10);
        builder.Property(e => e.UserId).HasColumnName("UserId");

    }
}