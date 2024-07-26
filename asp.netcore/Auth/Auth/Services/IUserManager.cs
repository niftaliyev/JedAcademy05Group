using Auth.ViewModels;

namespace Auth.Services;

public interface IUserManager
{
    public UserCredentials CurrentUser { get; set; }
    bool Login(string username, string password);
    UserCredentials GetCredentials();
}
