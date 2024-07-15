namespace BlogWebSite.Models;

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public IEnumerable<Post> Posts { get; set; }
}
