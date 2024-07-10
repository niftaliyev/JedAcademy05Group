using BlogWebSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebSite.Controllers;

public class PostsController : Controller
{
    private readonly ApplicationDbContext context;

    public PostsController(ApplicationDbContext context)
    {
        this.context = context;
    }
    public IActionResult Posts()
    {
        var posts = context.Posts.ToList();
        return View(posts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Post post)
    {
        if (string.IsNullOrEmpty(post.Content))
        {
            ModelState.AddModelError("Content","hani content ??");
        }
        if (ModelState.IsValid)
        {
            post.Date = DateTime.Now;
            context.Add(post);
            await context.SaveChangesAsync();
            TempData["Status"] = "New post added!";
            return RedirectToAction("Posts", "Posts");
        }
        return View(post);
    }
}
