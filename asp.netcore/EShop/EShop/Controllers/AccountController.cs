using EShop.Models.Identity;
using EShop.Services;
using EShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly BrevoEmailService emailService;

    public AccountController(UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager, BrevoEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        if (ModelState.IsValid)
        {
            var user = new AppUser 
            { 
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName, 
                Year = model.Year 
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //https://lcoalhost:5000/Account/ConfirmEmail?userId=1&token=blabla
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = Url.Action("ConfirmEmail", "Account",
                    new { userId = user.Id, token = token },
                    protocol: HttpContext.Request.Scheme);
                await emailService.SendEmailAsync(user.Email,"Please confirm your email", callBackUrl);
                return RedirectToAction("Login", "Account");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {   
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                    ModelState.AddModelError("loginForm", "Your account is locked!");
            }
            else  
                ModelState.AddModelError("loginForm","Please confirm your email!");           
        }
        else
            ModelState.AddModelError("loginForm", "Incorrect email or password");

        return View();
    }


    [HttpGet]
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId,string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user,token);
            if (result.Succeeded)
            {
                ViewBag.Message = "Email confirmed";
                return View();
            }
        }
        ViewBag.Message = "Error!";
        return View();
    }

    [HttpGet]
    public IActionResult AccessDenied(string returnUrl)
    {
        return View();
    }
}

