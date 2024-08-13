using TodoList.Core.Entities;

namespace TodoList.Core.Services;

public interface IToDoService
{
    void AddToDoList(string title);
    void AddTaskToList(string listId, string title);

    IEnumerable<ToDoItem> GetAllItemsByListId(string listId);
    IEnumerable<ToDoItemList> GetAllCurrentUserList();
}
