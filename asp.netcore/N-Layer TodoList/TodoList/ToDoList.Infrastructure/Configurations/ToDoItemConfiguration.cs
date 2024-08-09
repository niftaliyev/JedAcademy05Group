using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Core.Entities;

namespace ToDoList.Infrastructure.Configurations;

public class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
{
    public void Configure(EntityTypeBuilder<ToDoItem> builder)
    {
        builder.Property(u => u.Id)
            .IsRequired()
            .HasDefaultValueSql("NEWID()");
        builder.Property(x=> x.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x=> x.Description)
            .HasMaxLength(100);
    }
}
