using BlogWebSite.Helpers;
using BlogWebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

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
        var categories = context.Categories.ToList();
        ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "Name");
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Create(Post post,IFormFile Image)
    {
        post.ImageUrl = await FileUploadHelper.UploadAsync(Image);

        if (string.IsNullOrEmpty(post.Content))
        {
            ModelState.AddModelError("Content","hani content ??");
        }
        //if (ModelState.IsValid)
        //{
            post.Date = DateTime.Now;
            context.Add(post);
            await context.SaveChangesAsync();
            TempData["Status"] = "New post added!";
            return RedirectToAction("Posts", "Posts");
        //}
        return View(post);
    }

    [HttpGet]
    public IActionResult Details(int id) 
    {
        var post = context.Posts.Where(x => x.Id == id).FirstOrDefault();
        return View(post);
    }

    [HttpGet]
    public IActionResult CategorySearch(int categoryId)
    {
        var result = context.Posts.Where(x => x.CategoryId == categoryId).ToList();
        
        return View(result);
    }

    [HttpGet]
    public IActionResult Search(string title)
    {
        var result = context.Posts.Where(x => x.Title == title).ToList();
        return View(result);
    }
}
