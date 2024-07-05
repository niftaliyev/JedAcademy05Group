using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieApi.Models;
using MovieApi.Options;
using MovieApi.Services;
using MovieApi.ViewModels;
using System.Diagnostics;

namespace MovieApi.Controllers
{
    public class HomeController : Controller
    {
        private ISearch _search;
        public HomeController(ISearch search)
        {
            _search = search;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
        public async Task<IActionResult> Search(string title,int page=1)
        {
            //var result = movieApiService.Search(movieName);
            var result = await _search.Search(title, page);
            var model = new SearchViewModel
            {
                CurrentPage = page,
                Movies = result.Movies,
                TotalPages = (int)Math.Ceiling(result.TotalResults / 10.0),
                TotalResult = result.TotalResults,
                Title = title

            };
            return View(model);
        }

        public async Task<IActionResult> Movie(string id)
        {
            var result = await _search.SearchMovieByIdAsync(id);
            return View(result);
        }


        public async Task<IActionResult> Privacy(string id)
        {
            await Console.Out.WriteLineAsync(id+" blabla");

            ViewBag.Id = id ?? "test";

            return View();
        }
    }
}
