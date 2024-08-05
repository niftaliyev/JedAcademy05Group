using Microsoft.OpenApi.Models;

namespace StateTaxService.AIAT.Inventory.Configurations;

public static class ConfigureSwagger
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "StateTaxService.AIAT.Inventory",
                Version = "v1",
                Description = "State tax service AIAT Inventory."
            });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
            });
        });
    }
}
