using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Auth.Filters;

public class TestActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        context.HttpContext.Response.Cookies.Append("test",DateTime.Now.ToString());
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is Controller controller)
        {
            controller.ViewBag.FilterData = "testFilterData";
        }
    }
}
