using MovieApi.Models;

namespace MovieApi.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetReviewsAsync(string imdbID);
    Task AddReviewAsync(Review review);
}
