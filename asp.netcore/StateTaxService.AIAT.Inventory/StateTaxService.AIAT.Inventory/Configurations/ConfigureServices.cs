using Polly;
using Polly.Extensions.Http;
using StateTaxService.AIAT.Common.Models.Settings;
using StateTaxService.AIAT.Inventory.Services.Sessions;
using System.Net;


namespace StateTaxService.AIAT.Inventory.Configurations;

public static class ConfigureServices
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ISessionPermissionService, SessionPermissionService>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("SessionSettings").Get<SessionSettings>().BaseUrl);
        })
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
