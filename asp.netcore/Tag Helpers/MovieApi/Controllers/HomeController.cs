using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieApi.Models;
using MovieApi.Options;
using MovieApi.Services;
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

        public async Task<IActionResult> IndexAsync(string movieName = "Matrix")
        {
            //var result = movieApiService.Search(movieName);
            var result = await _search.Search(movieName);
            return View(result);
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
