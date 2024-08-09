using TodoList.Core.Entities;
using TodoList.Core.Repositories;

namespace ToDoList.Infrastructure.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly AplicationDbContext context;

    public ToDoItemRepository(AplicationDbContext context)
    {
        this.context = context;
    }
    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
    }

    public void Delete(ToDoItem item)
    {
        context.Remove(item);
    }

    public ToDoItem Get(string id)
    {
        var item = context.ToDoItems.Find(id);
        if (item == null)
            throw new Exception("Not found!");

        return item;
    }

    public IEnumerable<ToDoItem> GetAll()
    {
        return context.ToDoItems.ToList();
    }

    public IEnumerable<ToDoItem> GetAllByListId(string listId)
    {
        return context.ToDoItems.Where(x => x.ToDoListId == listId).ToList();
    }

    public void Update(ToDoItem item)
    {
        context.Update(item);
    }
}
