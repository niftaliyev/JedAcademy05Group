using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Models.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.
            Property(x => x.Tittle)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(new List<Category>
        {
            new Category { Id = 1, Tittle ="Azerbaijani"},
            new Category { Id = 2, Tittle ="Ukrainan"},
            new Category { Id = 3, Tittle ="Japanese"},
            new Category { Id = 4, Tittle ="Italian"}
        });
    }
}
