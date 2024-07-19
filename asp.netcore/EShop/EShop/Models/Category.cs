namespace EShop.Models;

public class Category
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
