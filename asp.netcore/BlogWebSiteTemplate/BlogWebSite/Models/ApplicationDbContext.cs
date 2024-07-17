using Microsoft.EntityFrameworkCore;

namespace BlogWebSite.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category{ CategoryId = 1, Name = "News"},
            new Category{ CategoryId = 2, Name = "Sport"},
            new Category{ CategoryId = 3, Name = "Business"}           
            );

        modelBuilder.Entity<Tag>().HasData(
                    new Tag { TagId = 1, Name = "Tag1" },
                    new Tag { TagId = 2, Name = "Tag2" },
                    new Tag { TagId = 3, Name = "Tag3" },
                    new Tag { TagId = 4, Name = "Tag4" });

        modelBuilder.Entity<Category>().HasKey(x => x.CategoryId);

        modelBuilder.Entity<PostTag>().HasKey(x => new { x.PostId,x.TagId });

        modelBuilder.Entity<PostTag>()
                                      .HasOne(x => x.Post)
                                      .WithMany(m => m.PostTags)
                                      .HasForeignKey(fk => fk.PostId);
        modelBuilder.Entity<PostTag>()
                                      .HasOne(x => x.Tag)
                                      .WithMany(m => m.PostTags)
                                      .HasForeignKey(fk => fk.TagId);
        
        base.OnModelCreating(modelBuilder);
    }
}
