using EShop.Models;
using EShop.Services;
using EShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;

public class OrdersController : Controller
{
    private readonly ICartService cartService;
    private readonly AplicationDbContext context;

    public OrdersController(ICartService cartService,AplicationDbContext context)
    {
        this.cartService = cartService;
        this.context = context;
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel orderViewModel)
    {
        if (ModelState.IsValid)
        {
            Order order = new Order();
            order.FullName = orderViewModel.FullName;
            order.Address = orderViewModel.Address;
            order.Phone = orderViewModel.Phone;
            order.TotalPrice = orderViewModel.TotalPrice;
            order.OrderStatus = orderViewModel.OrderStatus;
            order.Comment = orderViewModel.Comment;
            order.Email = orderViewModel.Email;

            var cartItems = cartService.GetProducts();
            order.TotalPrice = cartItems.Sum(x => x.Product.Price * x.Amount);
            order.OrderStatus = OrderStatus.New;
            context.Orders.Add(order);
            await context.SaveChangesAsync();

            var orderProducts = cartItems.Select(x => new OrderProduct{
                OrderId = order.Id,
                ProductId = x.Product.Id,
                Amount = x.Amount
            });
            context.OrderProducts.AddRange(orderProducts);
            await context.SaveChangesAsync();
            cartService.Clear();
            return RedirectToAction("Index", "Home");
        }
        return View(orderViewModel);
    }
}
