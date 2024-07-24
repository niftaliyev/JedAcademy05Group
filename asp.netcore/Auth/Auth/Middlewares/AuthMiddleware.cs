using Auth.Encryptors;
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

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Cookies.ContainsKey("auth"))
        {
            var hash = context.Request.Cookies["auth"];
            var json = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1911",hash);
            var user = JsonSerializer.Deserialize<UserCredentials>(json);
            if (user.Expiration > DateTime.Now)
            {
                await next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("GoodBye!!");
            }
        }
    }
}
