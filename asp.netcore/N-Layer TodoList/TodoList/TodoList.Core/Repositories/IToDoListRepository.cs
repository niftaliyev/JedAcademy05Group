using TodoList.Core.Base;
using TodoList.Core.Entities;

namespace TodoList.Core.Repositories;

public interface IToDoListRepository : IRepository<ToDoItemList>
{
    IEnumerable<ToDoItemList> GetAllByUserId(string userId);
}
