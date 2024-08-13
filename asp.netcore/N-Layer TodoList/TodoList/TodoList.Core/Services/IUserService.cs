using TodoList.Core.Entities;

namespace TodoList.Core.Services;

public interface IUserService
{
    User CurrentUser { get; }
}
