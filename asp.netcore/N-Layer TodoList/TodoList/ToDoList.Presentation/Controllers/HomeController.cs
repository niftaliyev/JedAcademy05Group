using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoList.Core.Services;
using ToDoList.Presentation.Models;
using ToDoList.Presentation.ViewModels;

namespace ToDoList.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoService toDoService;

        public HomeController(IToDoService toDoService)
        {
            this.toDoService = toDoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tasks(string id)
        {
            var viewModel = new TasksPageViewModel
            {
                CurrenListId = id,
                ToDoItemLists = toDoService.GetAllCurrentUserList(),
                CurrentListToDoItems = toDoService.GetAllItemsByListId(id)
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddList(AddToDoListViewModel NewList)
        {
            toDoService.AddToDoList(NewList.Title);
            TempData["Message"] = "new lsit added!";
            return RedirectToAction("Tasks","Home");
        }
        [HttpPost]
        public IActionResult AddItem(AddToDoItemViewModel NewItem)
        {
            toDoService.AddTaskToList(NewItem.ToDoListId,NewItem.Title);
            TempData["Message"] = "new item added!";
            return RedirectToAction("Tasks", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
