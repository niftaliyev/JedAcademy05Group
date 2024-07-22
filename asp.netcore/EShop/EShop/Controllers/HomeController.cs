using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace EShop.Controllers;

public class HomeController : Controller
{
    private readonly AplicationDbContext context;

    public HomeController(AplicationDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        return View(context.Products.ToList());
    }

    public IActionResult Privacy()
    {
        //if (!HttpContext.Request.Cookies.ContainsKey("sesion"))
        //{
        //    HttpContext.Response.Cookies.Append("sesion", Guid.NewGuid().ToString());
        //}
        HttpContext.Session.SetString("Date",DateTime.Now.ToString());
        return View();
    }

    public IActionResult AddToCart(int id,string returnUrl)
    {

        Dictionary<int, int> cart;
        if (!HttpContext.Session.Keys.Contains("cart"))
        {
            cart = new Dictionary<int, int>();
            cart.Add(id,1);
        }
        else
        {
            cart = JsonConvert.DeserializeObject<Dictionary<int,int>>(HttpContext.Session.GetString("cart"));

            if (cart.ContainsKey(id))
                cart[id]++;
            else 
                cart.Add(id,1);

        }
        HttpContext.Session.SetString("cart",JsonConvert.SerializeObject(cart));

        if (returnUrl == null)
            return RedirectToAction("Index", "Home");
        else
            return Redirect(returnUrl);
    }
}
