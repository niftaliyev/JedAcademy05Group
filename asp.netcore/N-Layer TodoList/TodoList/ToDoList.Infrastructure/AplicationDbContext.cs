using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoList.Core.Entities;

namespace ToDoList.Infrastructure;

public class AplicationDbContext : IdentityDbContext<IdentityUser>
{
    public AplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.ApplyConfiguration(new ToDoItemConfiguration());
        //builder.ApplyConfiguration(new ToDoItemListConfiguration());

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<ToDoItemList> ToDoList { get; set; }
}
