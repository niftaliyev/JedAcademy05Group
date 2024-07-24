using Auth.Encryptors;
using Auth.Models;
using Auth.Services;
using Auth.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Auth.Controllers;

public class AuthController : Controller
{
    private readonly ApplicationDbContext context;
    private readonly IUserManager userManager;

    public AuthController(ApplicationDbContext context, IUserManager userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            context.Users.Add(new Models.User
            {
                Login = model.Login,
                PasswordHash = Sha256Encryptor.Encrypt(model.Password),
                IsAdmin = false
            });
            await context.SaveChangesAsync();
            return RedirectToAction("Login","Auth");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (userManager.Login(model.Login,model.Password))
            {
                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("all","Incorrect login or password!");
        }
        return View();
    }

}
