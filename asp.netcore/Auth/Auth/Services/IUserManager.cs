namespace Auth.Services;

public interface IUserManager
{
    bool Login(string username, string password);
}
