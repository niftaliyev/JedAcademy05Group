using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EShop.Models.Configuration;

public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder
             .HasOne(x => x.Product)
             .WithMany(m => m.OrderProducts)
             .HasForeignKey(fk => fk.ProductId);
        builder
             .HasOne(x => x.Order)
             .WithMany(m => m.OrderProducts)
             .HasForeignKey(fk => fk.OrderId);
    }
}
