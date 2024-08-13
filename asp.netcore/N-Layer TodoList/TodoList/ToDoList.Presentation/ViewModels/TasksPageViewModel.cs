using TodoList.Core.Entities;

namespace ToDoList.Presentation.ViewModels;

public class TasksPageViewModel
{
    public string CurrenListId { get; set; }
    public IEnumerable<ToDoItemList> ToDoItemLists { get; set; } = new List<ToDoItemList>();
    public IEnumerable<ToDoItem> CurrentListToDoItems { get; set; } = new List<ToDoItem>();
    public AddToDoItemViewModel NewItem { get; set; }
    public AddToDoListViewModel NewList { get; set; }
}
