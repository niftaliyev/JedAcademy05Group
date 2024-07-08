using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext dbContext;

    public ReviewService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Review>> GetReviewsAsync(string imdbID)
    {
        var result = await dbContext.Reviews.Where(x => x.imdbID == imdbID).ToListAsync();
        return result;
    }
    public async Task AddReviewAsync(Review review)
    {
        dbContext.Reviews.Add(review);
        await dbContext.SaveChangesAsync();
    }

}
