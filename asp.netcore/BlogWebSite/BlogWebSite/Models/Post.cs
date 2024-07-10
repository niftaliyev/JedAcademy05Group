using System.ComponentModel.DataAnnotations;

namespace BlogWebSite.Models;

public class Post
{
    public int Id { get; set; }

    [Required(ErrorMessage = "hani title ??")]
    [StringLength(100,MinimumLength = 3)]
    [Display(Name = "Title")]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    public DateTime Date { get; set; }

    [Display(Name = "Image Url")]
    public string ImageUrl { get; set; }
}
