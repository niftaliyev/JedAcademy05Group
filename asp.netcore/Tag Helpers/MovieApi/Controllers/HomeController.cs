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
        private readonly IReviewService reviewService;

        public HomeController(ISearch search, IReviewService reviewService)
        {
            _search = search;
            this.reviewService = reviewService;
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
            var movie = await _search.SearchMovieByIdAsync(id);
            var rewies = await reviewService.GetReviewsAsync(id);

            var model = new MovieDetailsViewModel
            {
                Movie = movie,
                Reviews = rewies
            };
            return View(model);
        }


        public async Task<ActionResult> PostReview(Review review)
        {
            await reviewService.AddReviewAsync(review);
            return RedirectToAction("Movie", new { id = review.imdbID });
        }


        public async Task<IActionResult> Privacy(string id)
        {
            await Console.Out.WriteLineAsync(id+" blabla");

            ViewBag.Id = id ?? "test";

            return View();
        }
    }
}
