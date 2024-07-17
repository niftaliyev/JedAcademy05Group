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
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public IEnumerable<PostTag> PostTags { get; set; }
}
