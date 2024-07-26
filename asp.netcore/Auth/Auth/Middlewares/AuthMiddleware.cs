using Auth.Encryptors;
using Auth.Services;
using Auth.ViewModels;
using System.Text.Json;

namespace Auth.Middlewares;

public class AuthMiddleware
{
    private RequestDelegate next;
    public AuthMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserManager userManager)
    {
        userManager.GetCredentials();
        await next.Invoke(context);
    }
}
