using MovieApi.Models;

namespace MovieApi.ViewModels;

public class SearchViewModel
{
    public IEnumerable<Movie> Movies { get; set; }

    public string Title { get; set; }
    public int TotalResult { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }

}
