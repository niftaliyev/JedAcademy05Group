using System.Security.Claims;
using TodoList.Core.Entities;
using TodoList.Core.Services;

namespace ToDoList.Presentation.Services;

public class UserService : IUserService
{
    private readonly User currentUser;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext;
        currentUser = new User();
        currentUser.Id = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }


    public User CurrentUser => currentUser;
}
