namespace Auth.ViewModels;

public class UserCredentials
{
    public string Login { get; set; }
    public bool isAdmin { get; set; }
    public DateTime Expiration { get; set; }
}
