using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Core.Entities;

namespace ToDoList.Infrastructure.Configurations;

internal class ToDoItemListConfiguration : IEntityTypeConfiguration<ToDoItemList>
{
    public void Configure(EntityTypeBuilder<ToDoItemList> builder)
    {
        builder.Property(u => u.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");
        builder.Property(x => x.Tittle)
            .IsRequired()
            .HasMaxLength(100);
    }
}
