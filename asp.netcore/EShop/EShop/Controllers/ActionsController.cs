using EShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    public class ActionsController : Controller
    {
        private readonly ICartService cartService;

        public ActionsController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        public IActionResult DeleteInCart(int id)
        {
            cartService.RemoveInCart(id);
            return RedirectToAction("Index","Home");
        }
    }
}
