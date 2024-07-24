using System.ComponentModel.DataAnnotations;

namespace Auth.ViewModels;

public class LoginViewModel
{
    [Required]
    [MinLength(5)]
    public string Login { get; set; }
    [Required]
    [MinLength(5)]
    public string Password { get; set; }
}
