using TodoList.Core.Entities;
using TodoList.Core.Repositories;

namespace ToDoList.Infrastructure.Repositories;

public class ToDoListRepository : IToDoListRepository
{
    private readonly AplicationDbContext context;

    public ToDoListRepository(AplicationDbContext context)
    {
        this.context = context;
    }
    public void Create(ToDoItemList item)
    {
        context.ToDoList.Add(item);
    }

    public void Delete(ToDoItemList item)
    {
        context.ToDoList.Remove(item);
    }

    public ToDoItemList Get(string id)
    {
        var item = context.ToDoList.Find(id);

        if (item == null)
            throw new Exception("Not Found!");

        return item;
    }

    public IEnumerable<ToDoItemList> GetAll()
    {
        return context.ToDoList.ToList();
    }

    public IEnumerable<ToDoItemList> GetAllByUserId(string userId)
    {
        return context.ToDoList.Where(x => x.UserId == userId).ToList();
    }

    public void Update(ToDoItemList item)
    {
        context.Update(item);
    }
}
