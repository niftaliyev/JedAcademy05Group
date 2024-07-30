using EShop.ViewModels;

namespace EShop.Services;

public interface ICartService
{
    public void RemoveInCart(int id);
    public List<CartItemViewModel> GetProducts();
    public void Clear();
}
