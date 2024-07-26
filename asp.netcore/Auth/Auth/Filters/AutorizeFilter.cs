using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Filters;

public class AutorizeFilter : Attribute, IAuthorizationFilter
{
    //private readonly IUserManager userManager;

    //public AutorizeFilter(IUserManager userManager)
    //{
    //    this.userManager = userManager;
    //}
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        IUserManager userManager = context.HttpContext.RequestServices.GetRequiredService<IUserManager>();
        if (userManager.CurrentUser == null)
        {
            context.Result = new RedirectToActionResult("Error","Home",null);
        }
    }
}
