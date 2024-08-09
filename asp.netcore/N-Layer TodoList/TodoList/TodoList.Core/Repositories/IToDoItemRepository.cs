using TodoList.Core.Base;
using TodoList.Core.Entities;

namespace TodoList.Core.Repositories;

public interface IToDoItemRepository : IRepository<ToDoItem>
{
    IEnumerable<ToDoItem> GetAllByListId(string listId);
}
