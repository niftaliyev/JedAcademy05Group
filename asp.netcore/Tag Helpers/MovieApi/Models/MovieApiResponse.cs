using MovieApi.Converters;
using System.Text.Json.Serialization;

namespace MovieApi.Models;

public class MovieApiResponse
{
    [JsonPropertyName("Search")]
    public IEnumerable<Movie> Movies { get; set;}

    [JsonPropertyName("totalResults")]
    [JsonConverter(typeof(IntJsonConverter))]
    public int TotalResults { get; set; }
    public string Response { get; set; }
}



