using BlogWebSite.Helpers;
using BlogWebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        ViewBag.Tags = new MultiSelectList(context.Tags, "TagId", "Name");
        return View();
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Create(Post post,IFormFile Image, int[] tags)
    {
        post.ImageUrl = await FileUploadHelper.UploadAsync(Image);

        if (string.IsNullOrEmpty(post.Content))
        {
            ModelState.AddModelError("Content","hani content ??");
        }

            post.Date = DateTime.Now;
            context.Add(post);
            await context.SaveChangesAsync();

        foreach (var item in tags)
        {
            context.PostTags.Add(new PostTag { TagId = item,PostId = post.Id});
        }
        await context.SaveChangesAsync();
        TempData["Status"] = "New post added!";
            return RedirectToAction("Posts", "Posts");


        return View(post);
    }

    [HttpGet]
    public IActionResult Details(int id) 
    {
        var post = context.Posts
                          .Include(x => x.PostTags)
                          .ThenInclude(x => x.Tag)
                          .Include(x => x.Category)
                          .FirstOrDefault(x => x.Id == id);
            
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

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var post = context.Posts.Find(id);
        ViewData["CategoryId"] = new SelectList(context.Categories, "CategoryId", "Name");
        var selectedTagsIds = context.PostTags.Where(x => x.PostId == id).Select(x => x.TagId);
        ViewBag.Tags = new MultiSelectList(context.Tags, "TagId", "Name", selectedTagsIds);
        return View(post);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Post post, IFormFile Image, int[] tags)
    {
        if (Image != null)
        {
            var path = await FileUploadHelper.UploadAsync(Image);
            post.ImageUrl = path;
        }
        post.Date = DateTime.Now;
        context.Posts.Update(post);
        await context.SaveChangesAsync();
        TempData["Status"] = "Post edited!!";
        return RedirectToAction("Posts", "Posts");
    }
}
