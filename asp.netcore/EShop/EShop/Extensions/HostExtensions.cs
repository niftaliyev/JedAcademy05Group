using EShop.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace EShop.Extensions;

public static class HostExtensions
{
    public static async Task SeedDataAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var configuration = services.GetRequiredService<IConfiguration>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            var result = await roleManager.CreateAsync(new IdentityRole { Name = "Admin"});
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
        }

        var adminEmail = configuration["AdminAccount:Login"];
        var adminPassword = configuration["AdminAccount:Password"];

        var user = await userManager.FindByEmailAsync(adminEmail);

        if (user == null)
        {
            user = new AppUser
            {
                Email = adminEmail,
                EmailConfirmed = true,
                FullName = "Admin",
                Year = 1995,
                LockoutEnabled = true,
                UserName = adminEmail
            };
            var result = await userManager.CreateAsync(user,adminPassword);
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            result = await userManager.AddToRoleAsync(user,"Admin");
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
        }
    }
}
