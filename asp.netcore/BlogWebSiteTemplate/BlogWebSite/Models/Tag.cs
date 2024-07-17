namespace BlogWebSite.Models;

public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; }
    public IEnumerable<PostTag> PostTags { get; set; }
}
