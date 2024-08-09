using TodoList.Core.Repositories;

namespace TodoList.Core.Base;

public interface IUnitOfWork : IDisposable
{
    public IToDoItemRepository ToDoItemRepository { get; }
    public IToDoListRepository ToDoListRepository { get; }

    void Save();
}
