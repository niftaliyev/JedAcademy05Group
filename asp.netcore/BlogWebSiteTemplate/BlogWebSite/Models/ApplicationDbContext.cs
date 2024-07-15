using Microsoft.EntityFrameworkCore;

namespace BlogWebSite.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category{ CategoryId = 1, Name = "News"},
            new Category{ CategoryId = 2, Name = "Sport"},
            new Category{ CategoryId = 3, Name = "Business"}           
            );

        modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);

        base.OnModelCreating(modelBuilder);
    }
}
