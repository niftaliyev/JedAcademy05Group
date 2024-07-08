using MovieApi.Models;

namespace MovieApi.ViewModels;

public class MovieDetailsViewModel
{
    public Movie Movie { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
}
