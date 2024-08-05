using StateTaxService.AIAT.Common.Models.Settings;

namespace StateTaxService.AIAT.Inventory.Configurations;

public static class ConfigureOptions
{
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtSettings>().Bind(configuration.GetSection("JwtSettings"));
        services.AddOptions<SessionSettings>().Bind(configuration.GetSection("SessionSettings"));
    }
}
