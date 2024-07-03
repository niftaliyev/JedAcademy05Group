using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    public class PeopleController : Controller
    {
        public IActionResult GetPeopleList()
        {
            var people = new List<Person>
            {
                new Person
                {
                    Name = "Vusal",
                    Age = 19
                },
                new Person
                {
                    Name = "Mircefer",
                    Age = 17
                },
                new Person
                {
                    Name = "Naila",
                    Age = 26
                },
                new Person
                {
                    Name = "Gultac",
                    Age = 22
                }
            };
            return View(people);
        }
    }
}
