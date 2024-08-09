using TodoList.Core.Base;
using TodoList.Core.Repositories;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AplicationDbContext context;

    public UnitOfWork(AplicationDbContext context)
    {
        this.context = context;
    }
    public IToDoItemRepository ToDoItemRepository => new ToDoItemRepository(context);

    public IToDoListRepository ToDoListRepository => new ToDoListRepository(context);

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Save()
    {
        context.SaveChanges();
    }
}
