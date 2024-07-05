using MovieApi.Models;

namespace MovieApi.Services;

public interface ISearch
{
    Task<MovieApiResponse> Search(string movieName,int page);
    Task<Movie> SearchMovieByIdAsync(string id);
}
