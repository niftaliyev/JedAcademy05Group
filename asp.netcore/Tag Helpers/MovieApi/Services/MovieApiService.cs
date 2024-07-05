using Microsoft.Extensions.Options;
using MovieApi.Models;
using MovieApi.Options;
using System.Text.Json;

namespace MovieApi.Services;

public class MovieApiService : ISearch
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<MovieApiOptions> options;

    //private readonly IConfiguration configuration;
    private string apiKey;
    private string baseUrl;
    public MovieApiService(HttpClient httpClient, IOptions<MovieApiOptions> options /*, IConfiguration configuration*/)
    {
        _httpClient = httpClient;
        this.options = options;
        apiKey = options.Value.ApiKey;
        baseUrl = options.Value.BaseUrl;
        //this.configuration = configuration;
        //apiKey = configuration["MovieApi:ApiKey"];
        //baseUrl = configuration["MovieApi:BaseUrl"];
    }

    public async Task<MovieApiResponse> Search(string movieName,int page =1)
    {
        movieName = movieName.ToLower();
        string url = $"{baseUrl}?apikey={apiKey}&s={movieName}&page={page}";
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<MovieApiResponse>(json);
        return result;
    }

    public async Task<Movie> SearchMovieByIdAsync(string id)
    {
        string url = $"{baseUrl}?apikey={apiKey}&i={id}";
        var response = await _httpClient.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        var resultMovie = JsonSerializer.Deserialize<Movie>(json);
        return resultMovie;
    }
}
