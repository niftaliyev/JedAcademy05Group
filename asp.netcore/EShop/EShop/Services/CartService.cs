using EShop.Models;
using EShop.ViewModels;
using Newtonsoft.Json;

namespace EShop.Services;

public class CartService : ICartService
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly AplicationDbContext context;

    public CartService(IHttpContextAccessor httpContextAccessor, AplicationDbContext context)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.context = context;
    }
    public List<CartItemViewModel> GetProducts()
    {
        if (httpContextAccessor.HttpContext.Session.Keys.Contains("cart"))
        {
            var cart = JsonConvert.DeserializeObject<Dictionary<int, int>>
                    (httpContextAccessor.HttpContext.Session.GetString("cart"));
            var products = context.Products.Where(x => cart.Keys.Contains(x.Id));
            var result = products.Select(product =>
                new CartItemViewModel { Product = product, Amount = cart[product.Id] });
            return result.ToList();
        }
        return new List<CartItemViewModel>();
    }

    public void Clear()
    {
        httpContextAccessor.HttpContext.Session.Clear();
    }

    public void RemoveInCart(int id)
    {
        if (httpContextAccessor.HttpContext.Session.Keys.Contains("cart"))
        {
            var cart = JsonConvert.DeserializeObject<Dictionary<int, int>>
                    (httpContextAccessor.HttpContext.Session.GetString("cart"));
            if (cart[id] > 1)
            {
                cart[id]--;
            }
            else
            {
                cart.Remove(id);
            }
            httpContextAccessor.HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

        }
    }
}
