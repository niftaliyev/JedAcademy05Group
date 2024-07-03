using MovieApi.Options;
using MovieApi.Services;

namespace MovieApi.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMovieApi(this IServiceCollection services, Action<MovieApiOptions> options)
    {
        services.AddTransient<ISearch, MovieApiService>();
        services.AddHttpClient();
        services.Configure(options);
        return services;
    }
}
