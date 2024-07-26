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
    private readonly EmailService emailService;

    public AuthController(ApplicationDbContext context, IUserManager userManager, EmailService emailService)
    {
        this.context = context;
        this.userManager = userManager;
        this.emailService = emailService;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        Random random = new Random();   
        if (ModelState.IsValid)
        {
            var registerCode = random.Next(4, 4);
            context.Users.Add(new Models.User
            {
                Login = model.Login,
                PasswordHash = Sha256Encryptor.Encrypt(model.Password),
                IsAdmin = false,
                RegisterCode = registerCode
            });
            await emailService.SendEmailAsync("kamran.eilink@gmail.com", "test auth",$"Hello {model.Login} , tour register code is: {registerCode}");
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
