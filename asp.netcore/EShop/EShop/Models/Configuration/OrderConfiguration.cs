using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Models.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.
                Property(x => x.FullName)
                .HasMaxLength(100)
                .IsRequired();
        builder.
               Property(x => x.Phone)
               .HasMaxLength(100)
               .IsRequired();
        builder.
            Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();
        builder.
                Property(x => x.Address)
                .HasMaxLength(100)
                .IsRequired();
    }
}
