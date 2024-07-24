using System.ComponentModel.DataAnnotations;

namespace Auth.ViewModels;

public class RegisterViewModel
{
    [Required]
    [MinLength(5)]
    public string Login { get; set; }
    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [MinLength(5)]
    [DataType(DataType.Password)]
    public string PasswordAgain { get; set; }
}
