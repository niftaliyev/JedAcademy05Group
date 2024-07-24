using Auth.Encryptors;
using Auth.Models;
using Auth.ViewModels;
using System.Text.Json;

namespace Auth.Services;

public class UserManager : IUserManager
{
    private readonly ApplicationDbContext context;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserManager(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        this.httpContextAccessor = httpContextAccessor;
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
                Expiration = DateTime.Now.AddMinutes(1)
            };
            var jsonUser = JsonSerializer.Serialize(userCredentials);
            var hash = AesEncryptor.EncryptString("b14ca5898a4e4133bbce2ea2315a1911", jsonUser);
            httpContextAccessor.HttpContext.Response.Cookies.Append("auth", hash);
            return true;
        }
        return false;
    }
}
