using Auth.Encryptors;
using Auth.Models;
using Auth.ViewModels;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Auth.Services;

public class UserManager : IUserManager
{
    private readonly ApplicationDbContext context;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserCredentials CurrentUser { get; set; }

    public UserManager(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        this.httpContextAccessor = httpContextAccessor;
    }


    public UserCredentials GetCredentials()
    {
        try
        {
            if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("auth"))
            {
                var hash = httpContextAccessor.HttpContext.Request.Cookies["auth"];
                var json = AesEncryptor.DecryptString("b14ca5898a4e4133bbce2ea2315a1911", hash);
                var user = JsonSerializer.Deserialize<UserCredentials>(json);
                if (user.Expiration > DateTime.Now)
                {
                    CurrentUser = user;
                    return CurrentUser;
                }
            }
        }
        catch (Exception)
        {
        }
        return null;
    }

    public bool Login(string username, string password)
    {
        var passwordHash = Sha256Encryptor.Encrypt(password);
        var user = context.Users.FirstOrDefault(x => x.Login == username &&
                                                                x.PasswordHash == passwordHash);
        if (user != null)
        {
            var userCredentials = new UserCredentials
            {
                Login = user.Login,
                isAdmin = user.IsAdmin,
                Expiration = DateTime.Now + TimeSpan.FromMinutes(5)
            };
            var jsonUser = JsonSerializer.Serialize(userCredentials);
            var hash = AesEncryptor.EncryptString("b14ca5898a4e4133bbce2ea2315a1911", jsonUser);
            httpContextAccessor.HttpContext.Response.Cookies.Append("auth", hash);
            return true;
        }
        return false;
    }
}
