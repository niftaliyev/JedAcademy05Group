using Microsoft.AspNetCore.Identity;

namespace EShop.Models.Identity;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public int Year { get; set; }
}
