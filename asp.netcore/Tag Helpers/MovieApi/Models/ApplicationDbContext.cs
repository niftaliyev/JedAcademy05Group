using Microsoft.EntityFrameworkCore;

namespace MovieApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Review> Reviews { get; set; }
}
